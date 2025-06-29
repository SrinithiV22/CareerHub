using System;
using System.IO;
using CareerHub.MyExceptions;

namespace CareerHub.ExceptionPrograms
{
    public class FileUpload
    {
        public void UploadResume(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileUploadException("File not found!");

                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 2 * 1024 * 1024) // 2MB limit
                    throw new FileUploadException("File size exceeded 2MB.");

                if (!(fileInfo.Extension == ".pdf" || fileInfo.Extension == ".docx"))
                    throw new FileUploadException("File format not supported. Only PDF and DOCX allowed.");

                Console.WriteLine("File uploaded successfully.");
            }
            catch (FileUploadException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

