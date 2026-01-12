using Blazored.LocalStorage;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using TaskyRevamp.Client.Services;
using TaskyRevamp.Client.Shared;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Client
{
	public class FileManagementService
	{
		public IJSRuntime JS;
		IStringLocalizer<SharedResources> Loc;
		private IOptions<MySettings> _mySettings;
		private readonly ILocalStorageService _localStorage;
		private readonly HashSet<string> SupportedAttachmentExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{ ".pdf", ".docx", ".doc", ".ppt", ".pptx", ".jpeg", ".jpg", ".png", ".txt", ".csv", ".json", ".xml"};
		private readonly HashSet<string> PreviewableExtensions = new(StringComparer.OrdinalIgnoreCase)
		{".pdf", ".jpeg", ".jpg", ".png", ".txt", ".csv", ".json", ".xml"};
		private readonly HashSet<string> AllowedUploadedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{ ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv", ".jpg", ".jpeg", ".png" };
		public FileManagementService(IOptions<MySettings> mySettings, IJSRuntime js, ILocalStorageService localStorage, IStringLocalizer<SharedResources> loc)
		{
			JS = js;
			_localStorage = localStorage;
			_mySettings = mySettings;
			Loc = loc;
		}
		public ValidationResult ValidateAttachment(IBrowserFile file, long maxSize)
		{
			var extension = Path.GetExtension(file.Name);
			if (!SupportedAttachmentExtensions.Contains(extension))
			{
				return new ValidationResult
				{
					NotValid = true,
					ValidationMessage = Loc["UnsupportedFileType"]
				};
			}
			if (file.Size > maxSize)
			{
				return new ValidationResult
				{
					NotValid = true,
					ValidationMessage = Loc["FileSizeExceedsLimit"]
				};
			}
			return new ValidationResult { NotValid = false };
		}
		public ValidationResult ValidateUploadedFile(IBrowserFile file, long maxSize)
		{
			var extension = Path.GetExtension(file.Name);
			if (!AllowedUploadedExtensions.Contains(extension))
			{
				return new ValidationResult
				{
					NotValid = true,
					ValidationMessage = Loc["UnsupportedFileTypeTask"]
				};
			}
			if (file.Size > maxSize)
			{
				return new ValidationResult
				{
					NotValid = true,
					ValidationMessage = Loc["FileSizeExceedsLimitTask"]
				};
			}
			return new ValidationResult { NotValid = false };
		}

		public bool CanPreview(string fileName)
		{
			var ext = Path.GetExtension(fileName);
			return PreviewableExtensions.Contains(ext);
		}
		public async Task DownloadFile(byte[] bytes, string fileName)
		{
			if (bytes != null)
			{
				var base64 = Convert.ToBase64String(bytes);
				await JS.InvokeVoidAsync("saveFileFromBytes", fileName, base64);
			}
		}
		public async Task DownloadUsersTemplateAsync()
		{
			var culture = await _localStorage.GetItemAsStringAsync("BlazorCulture") ?? "en";
			string[] headers;
			string fileName;
			string sheetName;
			if (culture.StartsWith("ar"))
			{
				headers = new string[] { "البريد الإلكتروني للمستخدم", "اسم الإدارة", "اسم الصلاحية" };
				fileName = "قالب ربط المستخدمين.xlsx";
				sheetName = "قالب ربط المستخدمين";
			}
			else
			{
				headers = new string[] { "User Email", "Department Name", "Privilege Name" };
				fileName = "Link Users Template.xlsx";
				sheetName = "Link users Template";
			}
			using var memStream = new MemoryStream();
			using (var spreadsheet = SpreadsheetDocument.Create(memStream, SpreadsheetDocumentType.Workbook))
			{
				var workbookPart = spreadsheet.AddWorkbookPart();
				workbookPart.Workbook = new Workbook();
				var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
				var sheetData = new SheetData();
				worksheetPart.Worksheet = new Worksheet(sheetData);
				var headerRow = new Row();
				foreach (var header in headers)
				{
					var cell = new Cell
					{
						DataType = CellValues.String,
						CellValue = new CellValue(header)
					};
					headerRow.AppendChild(cell);
				}
				sheetData.AppendChild(headerRow);
				var sheets = spreadsheet.WorkbookPart.Workbook.AppendChild(new Sheets());
				var sheet = new Sheet
				{
					Id = spreadsheet.WorkbookPart.GetIdOfPart(worksheetPart),
					SheetId = 1,
					Name = sheetName
				};
				sheets.Append(sheet);
				spreadsheet.WorkbookPart.Workbook.Save();
			}

			memStream.Position = 0;
			using var streamRef = new DotNetStreamReference(stream: memStream);
			await JS.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
		}
		public async Task InitDropZone(string dropZoneId, string inputFileId)
		{
			try
			{
				await JS.InvokeVoidAsync("fileService.initDropZone", dropZoneId, inputFileId);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error initializing drop zone: {ex.Message}");
			}
		}
		public async Task PreviewFile(byte[] bytes, string fileName, string contentType)
		{
			if (bytes == null) return;
			var base64 = Convert.ToBase64String(bytes);
			await JS.InvokeVoidAsync("previewFileFromBytes", fileName, base64, contentType);
		}
		public async Task<(byte[] Bytes, string FileName, string ContentType)> ReadFileAsync(IBrowserFile file)
		{
			using var stream = file.OpenReadStream(long.MaxValue);
			using var ms = new MemoryStream();
			await stream.CopyToAsync(ms);

			return (ms.ToArray(), file.Name, file.ContentType);
		}
		public List<string[]> ReadExcelRows(byte[] fileBytes)
		{
			var rowsList = new List<string[]>();

			using var ms = new MemoryStream(fileBytes);
			using var doc = SpreadsheetDocument.Open(ms, false);

			var sheet = doc.WorkbookPart!.Workbook.Sheets!.GetFirstChild<Sheet>();
			var worksheetPart = (WorksheetPart)doc.WorkbookPart.GetPartById(sheet.Id!);
			var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
			foreach (var row in sheetData.Elements<Row>())
			{
				var cells = row.Elements<Cell>().Select(c => GetCellValue(c, doc)).ToArray();
				rowsList.Add(cells);
			}
			return rowsList;
		}
		private string GetCellValue(Cell cell, SpreadsheetDocument doc)
		{
			if (cell.CellValue == null) return "";
			var value = cell.CellValue.Text;
			if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
			{
				var stringTable = doc.WorkbookPart.SharedStringTablePart?.SharedStringTable;
				if (stringTable != null)
				{
					return stringTable.ElementAt(int.Parse(value)).InnerText;
				}
			}
			return value;
		}
		public string GetFileIcon(string fileName) // edit later to use svg icons
		{
			var extension = Path.GetExtension(fileName)?.ToLower();
			return extension switch
			{
				".pdf" => "bi bi-file-pdf-fill text-danger",
				".docx" or ".doc" => "bi bi-file-word-fill text-primary",
				".ppt" or ".pptx" => "bi bi-file-ppt-fill text-warning",
				".jpeg" or ".jpg" or ".png" => "bi bi-file-image-fill text-info",
				".txt" => "bi bi-file-text-fill text-secondary",
				".csv" => "bi bi-file-spreadsheet-fill text-success",
				".json" or ".xml" => "bi bi-file-code-fill text-dark",
				_ => "bi bi-file-earmark-fill text-muted"
			};
		}
		public string GetMimeType(string fileName)
		{
			var ext = Path.GetExtension(fileName)?.ToLower();
			return ext switch
			{
				".pdf" => "application/pdf",
				".jpeg" or ".jpg" => "image/jpeg",
				".png" => "image/png",
				".txt" => "text/plain",
				".csv" => "text/csv",
				".json" => "application/json",
				".xml" => "application/xml",
				_ => "application/octet-stream"
			};
		}
		public string GetFileSize(long bytes)
		{
			string[] sizes = { "B", "KB", "MB" };
			double len = bytes;
			int order = 0;
			while (len >= 1024 && order < sizes.Length - 1)
			{
				order++;
				len = len / 1024;
			}
			return $"{len:0.##} {sizes[order]}";
		}
	}
}
