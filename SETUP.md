# Setup hướng dẫn chạy dự án (Backend + Frontend)

Dùng file này khi clone lại 2 repo (`EnterpriseManagement-Backend` và
`EnterpriseManagement-FE`) trên máy mới. Không chỉ có mỗi lệnh seed admin —
cần chạy đủ các bước bên dưới theo đúng thứ tự thì mới lên được.

## 0. Yêu cầu cài sẵn trên máy

- Docker Desktop (đang chạy)
- .NET SDK 10.0+
- Node.js 20+ và npm
- Git

## 1. Clone 2 repo (đặt cạnh nhau trong 1 thư mục cha)

```bash
git clone https://github.com/M0M0M0M0/EnterpriseManagement-Backend.git
git clone https://github.com/phanminhnhatPMN/EnterpriseManagement-FE.git
```

## 2. Bật database bằng Docker

```bash
cd EnterpriseManagement-Backend
docker compose up -d
```

Container SQL Server sẽ chạy ở cổng `1433` (user `sa`, password
`YourStrong!Passw0rd` — khai báo sẵn trong `docker-compose.yml` /
`appsettings.json`).

## 3. Cài `dotnet-ef` (chỉ cần làm 1 lần trên máy, nếu chưa có)

```bash
dotnet tool install --global dotnet-ef
```

Kiểm tra đã cài chưa:

```bash
dotnet ef --version
```

## 4. Tạo bảng trong database (chạy migration)

Chạy từ thư mục `EnterpriseManagement-Backend`:

```bash
dotnet ef database update --project EnterpriseManagement.Infrastructure --startup-project EnterpriseManagement.Api
```

Lệnh này tạo toàn bộ bảng theo các migration đã có sẵn trong repo. Chỉ cần
chạy lại nếu có migration mới, hoặc database bị xóa/tạo lại từ đầu.

## 5. Chạy Backend API

```bash
cd EnterpriseManagement.Api
dotnet run
```

- API chạy tại `http://localhost:5068`
- **Lần chạy đầu tiên** (khi bảng `Roles`/`Users` đang rỗng), `DataSeeder` sẽ
  tự động tạo:
  - 3 role: `ADMIN`, `MANAGER`, `EMPLOYEE`
  - 1 tài khoản admin: username `admin` / password `admin`
    (mật khẩu yếu, chỉ dùng để dev local — không dùng ngoài môi trường thật)

Không cần chạy thêm lệnh seed riêng nào — seed tự chạy mỗi lần start API,
và chỉ tạo dữ liệu khi bảng đang rỗng nên chạy lại nhiều lần cũng an toàn.

## 6. Chạy Frontend

Mở terminal khác, từ thư mục `EnterpriseManagement-FE`:

```bash
npm install
npm run dev
```

- App chạy tại `http://localhost:5173`
- Đăng nhập bằng `admin` / `admin`

Frontend gọi API qua đường dẫn tương đối `/api`, được Vite tự động proxy
sang `http://localhost:5068` (cấu hình trong `vite.config.ts`, áp dụng cho cả
`npm run dev` và `npm run preview`) — không cần chỉnh gì thêm.

## Lưu ý

- Backend đang cấu hình CORS chỉ cho phép `http://localhost:5173` và
  `http://127.0.0.1:5173` (xem `Program.cs`). Nếu FE chạy ở cổng khác thì
  phải thêm origin đó vào `AddCors`.
- Muốn xóa sạch database và làm lại từ đầu:
  ```bash
  docker compose down -v
  ```
  rồi lặp lại từ bước 2.
- Danh sách cổng đang dùng: `1433` (SQL Server), `5068` (API), `5173`
  (Frontend dev server).
