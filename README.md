# Quanlynhansu
# Quản Lý Nhân Sự

**Phần mềm Quản Lý Nhân Sự** được xây dựng bằng ngôn ngữ lập trình **C#** sử dụng **WinForms** (Windows Forms) và **.NET Framework**. Dự án này nhằm mục đích cung cấp giải pháp hiệu quả cho việc quản lý nhân sự trong các tổ chức và doanh nghiệp, từ việc quản lý thông tin nhân viên đến việc theo dõi công việc, chấm công và tạo báo cáo chi tiết.

Phần mềm hỗ trợ các chức năng chính như thêm, sửa, xóa thông tin nhân viên và phòng ban, chấm công và tạo các báo cáo về nhân sự và hiệu suất công việc. Tất cả các thao tác đều được thực hiện thông qua giao diện người dùng thân thiện, giúp người quản lý dễ dàng thao tác mà không gặp phải khó khăn nào.

Link Figma: https://www.figma.com/design/nfJisMPrN7gEpzjwyoTWfU/THIET-KE-GIAO-DIEN-HE-THONG-QUAN-LY-NHAN-SU?t=LWs4Gi6diPg3MyTM-0

## Tính Năng Chính

### 1. **Quản Lý Thông Tin Nhân Viên**
   - **Thêm nhân viên**: Cho phép thêm nhân viên mới vào hệ thống, nhập các thông tin cơ bản như tên, ngày sinh, địa chỉ, số điện thoại, email, chức vụ và phòng ban.
   - **Chỉnh sửa thông tin nhân viên**: Cập nhật các thông tin của nhân viên khi có sự thay đổi, ví dụ như thay đổi chức vụ, phòng ban hoặc thông tin liên lạc.
   - **Xóa nhân viên**: Loại bỏ nhân viên khỏi hệ thống khi họ không còn làm việc tại công ty.
   - **Tìm kiếm nhân viên**: Tính năng tìm kiếm giúp dễ dàng tra cứu thông tin nhân viên dựa trên các tiêu chí như tên, mã nhân viên, phòng ban, hay chức vụ.

### 2. **Quản Lý Lịch Làm Việc**
   - **Lập lịch làm việc**: Cho phép người quản lý thiết lập lịch làm việc cho từng nhân viên hoặc phòng ban, bao gồm thời gian bắt đầu và kết thúc ca làm việc, số giờ làm việc mỗi ngày và tuần.
   - **Quản lý lịch làm việc thay đổi**: Điều chỉnh lịch làm việc khi có sự thay đổi như nghỉ phép, làm việc ngoài giờ, hoặc thay đổi ca làm việc.
   - **Theo dõi giờ làm việc**: Phần mềm giúp theo dõi giờ làm việc thực tế của nhân viên so với lịch làm việc đã thiết lập, phục vụ cho việc quản lý chấm công và tính lương.

### 3. **Quản Lý Chấm Công**
   - **Chấm công vào/ra**: Ghi nhận giờ vào và ra của nhân viên, theo dõi thời gian làm việc từng ngày.
   - **Chấm công tự động**: Tự động ghi nhận thời gian làm việc của nhân viên khi họ đăng nhập hoặc đăng xuất khỏi hệ thống.
   - **Theo dõi ngày nghỉ phép và nghỉ lễ**: Quản lý các ngày nghỉ của nhân viên, bao gồm nghỉ phép, nghỉ lễ, và các loại nghỉ khác theo yêu cầu của công ty.

### 4. **Quản Lý Lương**
   - **Tính lương**: Phần mềm hỗ trợ tính toán lương dựa trên số giờ làm việc, các ngày nghỉ, và các điều kiện tính lương khác của nhân viên. Tính lương có thể được cấu hình theo giờ làm việc thực tế hoặc theo hệ số lương.
   - **Bảng lương**: Xuất bảng lương cho từng nhân viên dựa trên dữ liệu chấm công và các quy định của công ty về lương thưởng, phụ cấp.
   - **Tính các khoản phụ cấp**: Tính các khoản phụ cấp (nếu có) như phụ cấp ăn trưa, phụ cấp đi lại, phụ cấp trách nhiệm.
   - **Điều chỉnh lương**: Người quản lý có thể thay đổi mức lương của nhân viên trong trường hợp có sự thay đổi về chức vụ, phòng ban hoặc điều kiện làm việc.


## Các Chức Năng Trong Ứng Dụng WinForms

Ứng dụng được xây dựng với **WinForms**, một công nghệ giao diện người dùng (UI) của .NET Framework, mang đến trải nghiệm người dùng trực quan và dễ sử dụng. Các chức năng cơ bản được tổ chức rõ ràng trong giao diện phần mềm, bao gồm:

- **Form chính**: Là màn hình chính của ứng dụng, cho phép người dùng truy cập nhanh chóng vào các tính năng chính như quản lý nhân viên, phòng ban, chấm công và báo cáo.
- **Các form con**: Các cửa sổ cho phép thêm, sửa, xóa và xem thông tin chi tiết của nhân viên, phòng ban, và các dữ liệu liên quan đến chấm công.
- **Menu điều hướng**: Các menu giúp người dùng dễ dàng chuyển qua lại giữa các chức năng khác nhau của phần mềm.
- **Dialog Boxes**: Thông báo và yêu cầu xác nhận khi người dùng thực hiện các thao tác quan trọng như xóa nhân viên hoặc sửa thông tin.

### Cấu Trúc Ứng Dụng

1. **Giao diện chính**:
   - Hiển thị các thông tin tổng quan về nhân viên, phòng ban và các báo cáo quan trọng.
   - Tích hợp các tab và menu để điều hướng đến các chức năng khác của hệ thống.

2. **Form thêm/sửa nhân viên**:
   - Giao diện người dùng cho phép nhập thông tin nhân viên mới hoặc chỉnh sửa thông tin nhân viên hiện tại.
   - Các trường nhập liệu rõ ràng, với các kiểu dữ liệu được xác thực (ví dụ: ngày sinh, số điện thoại).

3. **Form báo cáo**:
   - Cho phép người quản lý xuất báo cáo theo các tiêu chí khác nhau, như theo phòng ban, theo thời gian làm việc hoặc theo hiệu suất công việc.

## Yêu Cầu Hệ Thống

### 1. **Hệ Điều Hành**
   - Windows 7 trở lên.

### 2. **Phần Mềm**
   - **.NET Framework** phiên bản 4.5 hoặc cao hơn.
   - **Visual Studio** (để biên dịch và chạy ứng dụng).

### 3. **Phần Cứng**
   - **RAM**: 4GB trở lên.
   - **Ổ cứng**: 200MB dung lượng trống.

## Cách Cài Đặt

1. **Tải về mã nguồn**: Bạn có thể tải mã nguồn từ repository của dự án hoặc sử dụng bản phát hành mới nhất có sẵn.
2. **Giải nén và mở dự án**: Sau khi tải về, giải nén các tệp và mở dự án bằng Visual Studio.
3. **Biên dịch và chạy ứng dụng**:
   - Mở dự án trong Visual Studio.
   - Biên dịch ứng dụng và chạy để kiểm tra tính năng của phần mềm.

## Hướng Dẫn Sử Dụng

1. **Đăng nhập** vào hệ thống bằng tài khoản được cung cấp.
2. **Menu chính**: Các mục chính trong hệ thống sẽ bao gồm:
   - **Nhân viên**: Quản lý danh sách nhân viên.
   - **Phòng ban**: Quản lý các phòng ban của công ty.
   - **Chấm công**: Quản lý và ghi nhận thời gian làm việc của nhân viên.
   - **Báo cáo**: Tạo và xuất báo cáo chi tiết.
3. **Thực hiện thao tác**:
   - Chọn tính năng cần sử dụng từ menu.
   - Thực hiện các thao tác cần thiết (thêm, sửa, xóa, tạo báo cáo) và lưu các thay đổi.

## Đóng Góp
1. Nguyễn Thị Bích Ngọc
2. Trần Phương Đan
3. Trần Thị Bích Tuyền
4. Hồ Sỹ Đức Thành
5. Phạm Trần Diệu Khanh


Chúng tôi luôn chào đón các đóng góp từ cộng đồng! Nếu bạn muốn đóng góp, vui lòng làm theo các bước dưới đây:

1. **Fork repository** của dự án.
2. **Tạo nhánh mới**: 
   - `git checkout -b feature/tinh-nang-moi`
3. **Commit thay đổi**: 
   - `git commit -m 'Thêm tính năng mới'`
4. **Push thay đổi lên nhánh**: 
   - `git push origin feature/tinh-nang-moi`
5. **Tạo Pull Request**: Tạo Pull Request để đóng góp tính năng mới vào dự án.

## License

Dự án này được phát hành dưới giấy phép [MIT](LICENSE).

## Liên Hệ

Nếu bạn có bất kỳ câu hỏi hoặc góp ý nào, vui lòng liên hệ qua email: `2ndevdes@gmail.com`.

---

Cảm ơn bạn đã sử dụng phần mềm **Quản Lý Nhân Sự**!
