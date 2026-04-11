import { Editor } from "https://esm.sh/@tiptap/core"
import StarterKit from "https://esm.sh/@tiptap/starter-kit"
import Underline from "https://esm.sh/@tiptap/extension-underline"
import TextAlign from "https://esm.sh/@tiptap/extension-text-align"
import Link from "https://esm.sh/@tiptap/extension-link"
import Strike from "https://esm.sh/@tiptap/extension-strike"
import Placeholder from "https://esm.sh/@tiptap/extension-placeholder"
import Image from "https://esm.sh/@tiptap/extension-image"
import Mention from "https://esm.sh/@tiptap/extension-mention"
import tippy from "https://esm.sh/tippy.js@6"
import { BulletList } from "https://esm.sh/@tiptap/extension-bullet-list";

window.tiptapEditor = window.tiptapEditor || {};
window.tiptapEditor.editors = {};
window.tiptapEditor.helpers = {};

window.tiptapEditor.init = function (id, content, dotNetHelper, placeholderText, users) {
    this.helpers[id] = dotNetHelper;
    this.users = users || [];
    const editor = new Editor({
        element: document.querySelector(`#${id}`),
        extensions: [
            StarterKit.configure({
                bulletList: false, // disable default bulletList
            }),
            CustomBulletList, // use our custom list
            Underline,
            CustomImage,
            Strike,
            Link.configure({ openOnClick: true}),
            TextAlign.configure({types: ['heading', 'paragraph']}),
            Placeholder.configure({placeholder: placeholderText || 'Write a comment...'}),
            Mention.configure({ HTMLAttributes: { class: 'mention', },
                suggestion: {
                    items: ({ query }) => {
                        const q = query.toLowerCase();
                        return window.tiptapEditor.users.filter(item => item.name?.toLowerCase().includes(q)).sort((a, b) =>
                            {
                                const aStarts = a.name.toLowerCase().startsWith(q);
                                const bStarts = b.name.toLowerCase().startsWith(q);
                                if (aStarts && !bStarts) return -1;
                                if (!aStarts && bStarts) return 1;
                                return a.name.localeCompare(b.name);
                            }).slice(0, 5);
                    },
                    render: () => {
                        let popup;
                        return {
                            onStart: props => {
                                if (!props.clientRect) return;
                                const container = document.createElement("div");
                                renderList(container, props);
                                popup = tippy(document.body, {
                                    getReferenceClientRect: props.clientRect,
                                    appendTo: () => document.body,
                                    content: container,
                                    showOnCreate: true,
                                    interactive: true,
                                    trigger: "manual",
                                    placement: "bottom-start",
                                });
                            },
                            onUpdate(props) {
                                if (!props.clientRect) return;
                                if (!popup) return;
                                renderList(popup.popper.querySelector(".dropdown-menu") || popup.popper, props);
                                popup.setProps({getReferenceClientRect: props.clientRect});
                            },
                            onExit() {
                                if (popup) {
                                    if (Array.isArray(popup)) {
                                        popup.forEach(p => p.destroy());
                                    }
                                    else {
                                        popup.destroy();
                                    }
                                    popup = null;
                                }
                            }
                        }
                    }
                }
            })
        ],
        content: content || "",
        parseOptions: {
            preserveWhitespace: 'full'
        },
        editorProps: {
            handlePaste() {
                return false;
            }
        },
        onUpdate: ({ editor }) => {
            const html = editor.getHTML();
            if (html.length > 100000) return;
            this.helpers[id].invokeMethodAsync("OnContentChanged", html);
        }
    });
    this.editors[id] = editor;
};
const CustomBulletList = BulletList.extend({
    addAttributes() {
        return {
            ...this.parent?.(),
            style: {
                default: 'list-style-type: disc; margin-left: 20px;',
                parseHTML: element => element.getAttribute('style'),
                renderHTML: attributes => ({ style: attributes.style }),
            },
        };
    },
});
function renderList(container, props) {
    container.innerHTML = `
        <div class="dropdown-menu show shadow-sm p-1" style="min-width:200px;">
            ${props.items.length === 0
            ? `<span class="dropdown-item text-muted">No results</span>`
            : props.items.map(item => `
                    <button type="button" class="dropdown-item mention-item">
                        ${item.name}
                    </button>
                `).join("")
        }
        </div>`;
    const buttons = container.querySelectorAll(".mention-item");
    buttons.forEach((btn, index) => {
        btn.addEventListener("click", () => {
            const item = props.items[index];
            props.command({ id: item.id, label: item.name });
        });
    });
};

window.tiptapEditor.setContent = function (id, content) {
    const editor = window.tiptapEditor.getEditor(id);
    if (editor) {
        editor.commands.setContent(content || "");
    }
};

window.tiptapEditor.getEditor = function (id) {
    return this.editors[id];
};

window.tiptapEditor.destroy = function (id) {
    const editor = window.tiptapEditor.getEditor(id);
    if (editor) {
        editor.destroy();
        delete window.tiptapEditor.editors[id];
        delete window.tiptapEditor.helpers[id];
    }
};

window.tiptapEditor.focus = function (id) {
    const editor = window.tiptapEditor.getEditor(id);
    if (editor) {
        editor.chain().focus().run();
    }
};
window.tiptapEditor.setContent = function (id, content) {
    const editor = window.tiptapEditor.getEditor(id);
    if (editor) {
        editor.commands.setContent(content || "", false); // false = do not parse as transaction
        // Force update styles for existing lists
        editor.view.dom.querySelectorAll('ul').forEach(ul => {
            ul.style.listStyleType = 'disc';
            ul.style.marginLeft = '20px';
        });
    }
};
window.tiptapEditor.isActive = function (id, type) {
    const editor = window.tiptapEditor.getEditor(id);
    if (!editor) return false;

    switch (type) {
        case "bold": return editor.isActive("bold");
        case "italic": return editor.isActive("italic");
        case "underline": return editor.isActive("underline");
        case "strike": return editor.isActive("strike");
        case "bulletList": return editor.isActive("bulletList");
        case "orderedList": return editor.isActive("orderedList");
        case "left": return editor.isActive({ textAlign: "left" });
        case "center": return editor.isActive({ textAlign: "center" });
        case "right": return editor.isActive({ textAlign: "right" });
        default: return false;
    }
};

window.tiptapEditor.pickImage = async function (id) {
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.accept = "image/*";

    fileInput.onchange = async () => {
        const file = fileInput.files[0];
        if (!file) return;

        // ✅ WAIT for resized image
        const base64 = await resizeImage(file);

        const editor = window.tiptapEditor.getEditor(id);
        if (editor) {
            editor.chain().focus().setImage({
                src: base64,
                style: 'border:1px solid gray; width:60%; max-width:70%;'
            }).run();
        }
    };

    fileInput.click();
};
function resizeImage(file, maxWidth = 500, maxHeight = 300) {
    return new Promise((resolve) => {
        const img = new window.Image(); // ✅ important fix
        const reader = new FileReader();

        reader.onload = e => img.src = e.target.result;

        img.onload = () => {
            const canvas = document.createElement('canvas');
            let { width, height } = img;

            if (width > maxWidth) {
                height = height * (maxWidth / width);
                width = maxWidth;
            }

            if (height > maxHeight) {
                width = width * (maxHeight / height);
                height = maxHeight;
            }

            canvas.width = width;
            canvas.height = height;

            const ctx = canvas.getContext('2d');
            ctx.drawImage(img, 0, 0, width, height);

            resolve(canvas.toDataURL('image/jpeg', 0.7)); // ✅ compressed
        };

        reader.readAsDataURL(file);
    });
}
const CustomImage = Image.extend({
    inline: false,
    addOptions() {
        return {
            ...this.parent?.(),
            allowBase64: true
        };
    },
    addAttributes() {
        return {
            ...this.parent?.(),
            style: {
                default: 'border:1px solid #ccc; width:90%; max-width:100%;',
                parseHTML: element => element.getAttribute('style'),
                renderHTML: attributes => ({ style: attributes.style })
            },
            width: {
                default: null,
                parseHTML: element => element.getAttribute('width'),
                renderHTML: attributes => ({ width: attributes.width })
            },
            height: {
                default: null,
                parseHTML: element => element.getAttribute('height'),
                renderHTML: attributes => ({ height: attributes.height })
            }
        };
    }
});
document.addEventListener("click", function (e) {
    const editorEl = e.target.closest(".editor-area");
    if (editorEl) {
        const id = editorEl.id;
        const editor = window.tiptapEditor.getEditor(id);
        if (editor) {
            editor.chain().focus().run();
        }
    }
});
console.log("Users array:", window.tiptapEditor.users);
window.tiptapEditor.bold = id => window.tiptapEditor.getEditor(id)?.chain().focus().toggleBold().run();
window.tiptapEditor.italic = id => window.tiptapEditor.getEditor(id)?.chain().focus().toggleItalic().run();
window.tiptapEditor.underline = id => window.tiptapEditor.getEditor(id)?.chain().focus().toggleUnderline().run();
window.tiptapEditor.strike = id => window.tiptapEditor.getEditor(id)?.chain().focus().toggleStrike().run();
window.tiptapEditor.alignLeft = id => window.tiptapEditor.getEditor(id)?.chain().focus().setTextAlign('left').run();
window.tiptapEditor.alignCenter = id => window.tiptapEditor.getEditor(id)?.chain().focus().setTextAlign('center').run();
window.tiptapEditor.alignRight = id => window.tiptapEditor.getEditor(id)?.chain().focus().setTextAlign('right').run();
window.tiptapEditor.bulletList = id => window.tiptapEditor.getEditor(id)?.chain().focus().toggleBulletList().run();
window.tiptapEditor.orderedList = id => window.tiptapEditor.getEditor(id)?.chain().focus().toggleOrderedList().run();
window.tiptapEditor.setLink = function (id, url) { window.tiptapEditor.getEditor(id)?.chain().focus().setLink({ href: url }).run(); };
