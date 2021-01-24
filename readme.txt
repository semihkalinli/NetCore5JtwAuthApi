DB First ile crud işlemleri oluşturmak için db'den modelleri getirme işlemi
Scaffold-DbContext "Server=localhost;Database=JwtBasicTwo;Trusted_Connection=True; User ID=sa;Password=pass" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Project "Autoking.Task2.Core"

Model yenileme
Scaffold-DbContext "Server=localhost;Database=JwtBasicTwo;Trusted_Connection=True; User ID=sa;Password=pass" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f -Project "Autoking.Task2.Core"