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

window.tiptapEditor = window.tiptapEditor || {};
window.tiptapEditor.editors = {};
window.tiptapEditor.helpers = {};

window.tiptapEditor.init = function (id, content, dotNetHelper, placeholderText, users) {
    this.helpers[id] = dotNetHelper;
    this.users = users || [];
    const editor = new Editor({
        element: document.querySelector(`#${id}`),
        extensions: [
            StarterKit,
            Underline,
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
        editorProps: {
            handlePaste() {
                return false;
            }
        },
        onUpdate: ({ editor }) => {
            const html = editor.getHTML();
            this.helpers[id].invokeMethodAsync("OnContentChanged", html);
        }
    });
    this.editors[id] = editor;
};

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

window.tiptapEditor.uploadImage = async function (id, file) {
    const editor = window.tiptapEditor.getEditor(id);
    if (!editor) return;
    const formData = new FormData();
    formData.append("file", file);
    const response = await fetch("/api/upload", {
        method: "POST",
        body: formData
    });
    const data = await response.json();
    editor.chain().focus().setImage({ src: data.url }).run();
};

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