Enable-Migrations -ProjectName "FA.JustBlog.Data"

Add-Migration -Name "Migrations" -StartUpProjectName "FA.JustBlog.WebMVC" -ProjectName "FA.JustBlog.Data" -ConfigurationTypeName "Configuration"

Update-Database -StartUpProjectName "FA.JustBlog.WebMVC" -ProjectName "FA.JustBlog.Data" -ConfigurationTypeName "Configuration"