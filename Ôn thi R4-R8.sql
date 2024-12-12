USE QL_NHANSU
GO

-- Tạo dữ liệu dump cho các bảng trong cơ sở dữ liệu.
-----------------------------------------------------------------------------------------------------------------------------------------------------
-- Thủ tục thêm dữ liệu vào bảng NHANVIEN
CREATE OR ALTER PROCEDURE PR_NHANVIEN
AS
BEGIN
    DECLARE @i INT = 1
    DECLARE @Ho NVARCHAR(50), @Dem NVARCHAR(50), @Ten NVARCHAR(50)
    DECLARE @DiaChi NVARCHAR(255)
    DECLARE @CCCD VARCHAR(50)
    
    -- Danh sách họ, đệm và tên để tạo tên tiếng Việt
    DECLARE @HoList NVARCHAR(50) = N'Nguyễn,Trần,Lê,Phạm,Hoàng,Phan,Võ,Vũ,Đặng,Bùi,Đỗ,Hồ,Ngô,Dương,Đinh'
    DECLARE @DemList NVARCHAR(50) = N'Văn,Hữu,Quốc,Thị,Minh,Thành,Bảo,Ngọc,Linh,Diệu,Phương,Hoàng,Kim'
    DECLARE @TenList NVARCHAR(50) = N'An,Bình,Chi,Dung,Đạt,Hà,Khánh,Linh,Minh,Nam,Phong,Quân,Trang,Tuấn,Vân,
									Ý,Khanh,Đan,Ngọc,Thành,Tuyền,Ngân,Loan,Giang,Danh'

    -- Danh sách địa chỉ tại Đà Nẵng
    DECLARE @QuanHuyenList NVARCHAR(255) = N'Liên Chiểu,Hải Châu,Ngũ Hành Sơn,Thanh Khê,Sơn Trà,Cẩm Lệ,Hoà Vang'

	DECLARE @DuongList NVARCHAR(255) = N'Nguyễn Văn Linh,Điện Biên Phủ,Lê Duẩn,Hoàng Diệu,Hùng Vương,
										Trần Phú,Phan Châu Trinh,Ngô Quyền,Ông Ích Khiêm,Nguyễn Văn Thoại,
										Hoài Thanh,Phan Tứ,Bạch Đằng,Trần Bạch Đằng'

    WHILE @i <= 1200
    BEGIN
        -- Chọn họ, đệm và tên ngẫu nhiên từ các danh sách
        SET @Ho = (SELECT value FROM STRING_SPLIT(@HoList, ',') ORDER BY NEWID() OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY)
        SET @Dem = (SELECT value FROM STRING_SPLIT(@DemList, ',') ORDER BY NEWID() OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY)
        SET @Ten = (SELECT value FROM STRING_SPLIT(@TenList, ',') ORDER BY NEWID() OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY)

        -- Chọn địa chỉ ngẫu nhiên tại Đà Nẵng
		SET @DiaChi = N'Số ' + CAST(1 + ABS(CHECKSUM(NEWID()) % 300) AS NVARCHAR(10)) + N', Đường ' + 
                      (SELECT value FROM STRING_SPLIT(@DuongList, ',') ORDER BY NEWID() OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY) + 
                      N', ' + (SELECT value FROM STRING_SPLIT(@QuanHuyenList, ',') ORDER BY NEWID() OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY) + 
                      N', Đà Nẵng'
        -- Tạo CCCD ngẫu nhiên (12 chữ số)
        SET @CCCD = CAST(100000000000 + ABS(CHECKSUM(NEWID()) % 900000000000) AS VARCHAR(50))

        INSERT INTO NHANVIEN (MANV, TENNV, NGAYSINH, DIACHI, SDT, CCCD, CHUCVU)
        VALUES (
            'NV' + RIGHT('0000' + CAST(@i AS VARCHAR(4)), 4), -- MANV
            @Ho + ' ' + @Dem + ' ' + @Ten, -- Tên tiếng Việt ngẫu nhiên
            DATEADD(DAY, -ABS(CHECKSUM(NEWID()) % 9000) - 5840, GETDATE()), -- NgaySinh trước 2004
            @DiaChi, -- Địa chỉ ngẫu nhiên tại Đà Nẵng
            '0' + CAST(100000000 + ABS(CHECKSUM(NEWID()) % 900000000) AS VARCHAR(15)), -- SDT ngẫu nhiên
            @CCCD, -- CCCD ngẫu nhiên
            CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN N'Pha che' ELSE N'Phuc vu' END -- Chức vụ ngẫu nhiên
        )

        SET @i = @i + 1
    END
END
-- Thực thi thủ tục
EXEC PR_NHANVIEN
-- Kiểm tra kết quả
SELECT * FROM NHANVIEN



-------------------------------------------------------------------------------------------------------------------------------

-- Thủ tục thêm dữ liệu vào bảng TAIKHOAN
CREATE OR ALTER PROCEDURE PR_TAIKHOAN
AS
BEGIN
    DECLARE @i INT = 1
    WHILE @i <= 1200
    BEGIN
        INSERT INTO TAIKHOAN (MANV, MATKHAU)
        VALUES (
			'NV' + RIGHT('000' + CAST(@i AS VARCHAR(4)), 4), -- MANV
            'MK' + RIGHT('0000' + CAST(ABS(CHECKSUM(NEWID()) % 10000) AS VARCHAR(4)), 4))
        SET @i = @i + 1
    END
END
-- Thực thi thủ tục
exec PR_TAIKHOAN
select * from TAIKHOAN

-------------------------------------------------------------------------------------------------------------------------------

-- Thủ tục thêm dữ liệu vào bảng CALAM
CREATE OR ALTER PROCEDURE PR_CALAM
AS
BEGIN
    DECLARE @i INT = 1;
    DECLARE @MACA VARCHAR(10);
    DECLARE @NGAY_LV DATE;
    DECLARE @BUOI_LAM NVARCHAR(10);
    DECLARE @GIO_BATDAU TIME;
    DECLARE @GIO_KETTHUC TIME;
	DECLARE @SOGIOLAMVIEC INT

    WHILE @i <= 1000
    BEGIN
        -- Tạo mã ca ngẫu nhiên
        SET @MACA = 'CA' + RIGHT('000' + CAST(@i AS VARCHAR(4)), 4);  
        
        -- Tạo ngày làm việc ngẫu nhiên trong khoảng từ 01-01-2023 đến 31-12-2023
        SET @NGAY_LV = DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 365, '2023-01-01');  

        -- Chọn buổi làm ngẫu nhiên
        SET @BUOI_LAM = CASE 
                           WHEN @i % 3 = 0 THEN N'Sáng' 
                           WHEN @i % 3 = 1 THEN N'Chiều' 
                           ELSE N'Tối' 
                        END;

        -- Giờ bắt đầu và giờ kết thúc ngẫu nhiên dựa trên buổi làm việc
        IF @BUOI_LAM = N'Sáng'
        BEGIN
            SET @GIO_BATDAU = '06:00';
            SET @GIO_KETTHUC = '11:00';
        END
        ELSE IF @BUOI_LAM = N'Chiều'
        BEGIN
            SET @GIO_BATDAU = '12:00';
            SET @GIO_KETTHUC = '17:00';
        END
        ELSE -- Buổi tối
        BEGIN
            SET @GIO_BATDAU = '18:00';
            SET @GIO_KETTHUC = '23:00';
        END

        -- Chèn dữ liệu vào bảng CALAM
        INSERT INTO CALAM (MACA, NGAY_LV, BUOI_LAM, GIO_BATDAU, GIO_KETTHUC, SOGIOLAMVIEC)
        VALUES (@MACA, @NGAY_LV, @BUOI_LAM, @GIO_BATDAU, @GIO_KETTHUC, @SOGIOLAMVIEC);

        -- Tăng biến đếm
        SET @i = @i + 1;
    END
END;



Exec PR_CALAM
-- Kiểm tra kết quả
SELECT * FROM CALAM


-------------------------------------------------------------------------------------------------------------------------------

-- Thủ tục thêm dữ liệu vào bảng LICHLAM

CREATE OR ALTER PROCEDURE PR_LICHLAM
AS
BEGIN
    DECLARE @i INT = 1;
    DECLARE @TRANGTHAI INT;
    
    WHILE @i <= 1200
    BEGIN
        -- Xác định trạng thái ngẫu nhiên: 0 - Không đi làm, 1 - Đi làm, 2 - Làm trễ
        SET @TRANGTHAI = ABS(CHECKSUM(NEWID())) % 3;
        
        -- Chèn dữ liệu vào bảng LICHLAM
        INSERT INTO LICHLAM (MANV, MACA, TRANGTHAI)
        VALUES (
            'NV' + RIGHT('000' + CAST(@i AS VARCHAR(4)), 4),  -- MANV
            'CA' + RIGHT('000' + CAST(@i AS VARCHAR(4)), 4),  -- MACA
            @TRANGTHAI  -- TRANGTHAI: 0, 1, hoặc 2
        );

        -- Tăng biến đếm
        SET @i = @i + 1;
    END
END;


-- Thực thi thủ tục
EXEC PR_LICHLAM;

-- Kiểm tra kết quả
SELECT * FROM LICHLAM;


-------------------------------------------------------------------------------------------------------------------------------

-- Thủ tục thêm dữ liệu vào bảng BANGLUONG
CREATE OR ALTER PROCEDURE PR_BANGLUONG
AS
BEGIN
    DECLARE @Counter INT = 1;

    WHILE @Counter <= 1200
    BEGIN
        -- Tạo giá trị cho các cột
        DECLARE @MANV VARCHAR(10) = CONCAT('NV', RIGHT('000' + CAST(@Counter AS VARCHAR(4)), 4))
        
        -- Thiết lập BANGLUONG_DATE cho từng tháng, bắt đầu từ tháng hiện tại và lùi dần về trước
        DECLARE @BANGLUONG_DATE DATE = DATEADD(MONTH, -(@Counter % 12), GETDATE())

        DECLARE @LUONGCOBAN DECIMAL(18, 2) = ROUND(1000000 + (RAND() * 4000000), 0)
        DECLARE @TONGLUONG DECIMAL(18, 2) = @LUONGCOBAN + ROUND((RAND() * 2000000), 0)

        -- Kiểm tra tồn tại của nhân viên trước khi chèn dữ liệu
        IF EXISTS (SELECT 1 FROM NHANVIEN WHERE MANV = @MANV)
        BEGIN
            -- Chèn dữ liệu vào bảng BANGLUONG
            INSERT INTO BANGLUONG (MANV, BANGLUONG_DATE, LUONGCOBAN, TONGLUONG)
            VALUES (@MANV, @BANGLUONG_DATE, @LUONGCOBAN, @TONGLUONG)
        END

        -- Tăng biến đếm
        SET @Counter = @Counter + 1
    END
END

-- Thực thi thủ tục
EXEC PR_BANGLUONG;

-- Kiểm tra dữ liệu trong bảng BANGLUONG
SELECT * FROM BANGLUONG;

--------------------------------------------------------------------------------------------------------------------
-- Thủ tục thêm dữ liệu vào bảng CHAMCONG 
CREATE OR ALTER PROCEDURE PR_CHAMCONG
AS
BEGIN
    DECLARE @i INT = 1;
    DECLARE @MANV VARCHAR(10);
    DECLARE @CHAMCONG_DATE DATE;
    DECLARE @TONGSOGIOLAM INT;
    DECLARE @THUONG DECIMAL(18, 2);
    DECLARE @PHAT DECIMAL(18, 2);

    WHILE @i <= 1200
    BEGIN
        -- Tạo giá trị cho các cột
        SET @MANV = 'NV' + RIGHT('000' + CAST(@i AS VARCHAR(4)), 4);
        SET @CHAMCONG_DATE = DATEADD(DAY, @i % 30, '2023-01-01');  
        SET @TONGSOGIOLAM = ROUND(RAND() * 24, 0);  -- Đã sửa đổi
        SET @THUONG = ROUND(RAND() * 1000000, 2);  
        SET @PHAT = -ROUND(RAND() * 500000, 2); 

        -- Chèn dữ liệu vào bảng CHAMCONG
        IF EXISTS (SELECT 1 FROM NHANVIEN WHERE MANV = @MANV)
        BEGIN
            INSERT INTO CHAMCONG (MANV, CHAMCONG_DATE, TONGSOGIOLAM, THUONG, PHAT)
            VALUES (@MANV, @CHAMCONG_DATE, @TONGSOGIOLAM, @THUONG, @PHAT);
        END

        SET @i = @i + 1;
    END
END;


-- Thực thi thủ tục để thêm 1000 bản ghi vào bảng CHAMCONG
EXEC PR_CHAMCONG;

-- Kiểm tra kết quả
SELECT * FROM CHAMCONG;

----------------------------------------------------------------------------------------------------------------------------
-- 10 module trong cơ sở dữ liệu để phục vụ các thao tác xử lý dữ liệu 
----------------------------------------------------------------------------------------------------------------------------

﻿/*Trigger tự động cộng số công và tính số tiền phạt theo tháng dựa trên trạng thái đi làm của nhân viên  .

Mô tả: Khi nhân viên đăng ký ca làm và có đi làm vào ngày hôm đó (trạng thái :1) 
thì được ghi nhận số giờ làm bằng cách cộng số giờ làm việc vào tổng số giờ làm việc trong tháng
Nếu nhân viên không đi làm (trạng thái 0) tự động thêm tiền phạt 100k trong tháng và không được ghi nhận số giờ làm.
Nếu nhân viên đi làm trễ (trạng thái 2) và tổng số lần đi làm trễ trên 3 lần thì tự động thêm tiền phạt 100k trong tháng và vẫn được ghi nhận số giờ làm.

Note: trạng thái 1 là có đi làm và đi làm đúng giờ  , trạng thái 0 là không đi làm , trạng thái 2 là trễ giờ làm 

------------------------------------------------------------------------------------------------------------------
Bảng:LICHLAM
Loại: After
Sự kiện :Update
process:
	1.Lấy @TRANGTHAI , @MANV , @MACA từ inserted
	2.Lấy @Ngay_LV,@SOGIOLAMVIEC từ bảng CALAM theo @MACA
	2.Lấy @TONGCACHAMCONG từ bảng CHAMCONG theo @MANV và theo tháng và năm của @NGAY_LV
	3.Lấy từ bảng CALAM
	4.
		4.1. Nếu @TRANGTHAI=1 (nhân viên có đi làm và đi đúng giờ)
	      ------>Cập nhật bảng CHAMCONG: TONGCACHAMCONG = TONGCACHAMCONG + @SOGIOLAMVIEC
										 Điều kiện: MANV=@MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH
		4.2. Nếu @TRANGTHAI=0 (nhân viên không đi làm) 
		  ------>Cập nhật bảng CHAMCONG: PHAT=PHAT-100000
										 Điều kiện: MANV=@MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH
		4.3. Nếu @TRANGTHAI=2 (nhân viên đi làm trễ)
			 -Cập nhật bảng CHAMCONG :TONGCACHAMCONG = TONGCACHAMCONG + @SOGIOLAMVIEC
										 Điều kiện: MANV=@MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH
		     - Đếm số lần nhân viên đó đi trễ trong tháng
			 - Nếu nhân viên đó đi trễ >=3 lần trong tháng đó 
			 ------>Cập nhật bảng CHAMCONG :PHAT=PHAT-100000
										 Điều kiện: MANV=@MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH
*/

CREATE OR ALTER TRIGGER trg_TrangThai
ON LICHLAM 
AFTER UPDATE 
AS
BEGIN 
    DECLARE @TONGSOGIOLAM INT, 
            @TRANGTHAI INT,
            @MANV VARCHAR(10),
			@MACA_CALAM VARCHAR(10),
            @MACA VARCHAR(10),
            @SOGIOLAMVIEC INT, 
            @COUNT INT,
            @NGAY_LV DATE,
            @YEAR INT,
            @MONTH INT
			
    SELECT @TRANGTHAI = TRANGTHAI, @MANV = MANV, @MACA = MACA
    FROM INSERTED

	SELECT  @NGAY_LV = NGAY_LV, @SOGIOLAMVIEC = SOGIOLAMVIEC
	FROM CALAM
	WHERE  MACA=@MACA

    SET @YEAR = YEAR(@NGAY_LV)
    SET @MONTH = MONTH(@NGAY_LV)

    
    SELECT @TONGSOGIOLAM = TONGSOGIOLAM
    FROM CHAMCONG
    WHERE MANV = @MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH

    

    
    IF @TRANGTHAI = 1
    BEGIN 
        
        UPDATE CHAMCONG
        SET TONGSOGIOLAM = ISNULL(@TONGSOGIOLAM, 0) + ISNULL(@SOGIOLAMVIEC, 0)
        WHERE MANV = @MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH
    END 
    ELSE IF @TRANGTHAI = 0
    BEGIN 
        
        UPDATE CHAMCONG
        SET PHAT = ISNULL(PHAT, 0) - 100000
        WHERE MANV = @MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH;
    END 
    ELSE IF @TRANGTHAI = 2
    BEGIN 
        
		SELECT @COUNT = COUNT(L.MACA)
		FROM CALAM C JOIN LICHLAM L ON C.MACA=L.MACA
		WHERE YEAR(NGAY_LV) = @YEAR AND MONTH(NGAY_LV) = @MONTH AND TRANGTHAI=2 AND MANV=@MANV

		UPDATE CHAMCONG
        SET PHAT = ISNULL(PHAT, 0) - 100000
        WHERE MANV = @MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH
  
        IF @COUNT >= 3 
        BEGIN
            

			UPDATE CHAMCONG
			SET TONGSOGIOLAM = ISNULL(@TONGSOGIOLAM, 0) + ISNULL(@SOGIOLAMVIEC, 0)
            WHERE MANV = @MANV AND YEAR(CHAMCONG_DATE) = @YEAR AND MONTH(CHAMCONG_DATE) = @MONTH

        END 
    END 
END

/*TEST TRƯỜNG HỢP 3 */

-------TẠO CA LÀM VIỆC MỚI VỚI CÙNG THÁNG VÀ NĂM 


INSERT INTO CALAM (MACA, NGAY_LV, BUOI_LAM, GIO_BATDAU, GIO_KETTHUC)
VALUES ('CA2400', '2024-11-20', 'Sáng', '08:00', '12:00'),
	 ('CA2401', '2024-11-21', 'Sáng', '08:00', '12:00'),
	('CA2402', '2024-11-22', 'Sáng', '08:00', '12:00');

------NHÂN VIÊN ĐĂNG KÝ CA

INSERT INTO LICHLAM (MANV, MACA, TRANGTHAI)
VALUES 
('NV0002', 'CA2400', NULL),
('NV0002', 'CA2401', NULL),
('NV0002', 'CA2402', NULL)
-------NHÂN VIÊN UPDATE TRẠNG THÁI ĐI LÀM TRỄ 
UPDATE LICHLAM 
SET TRANGTHAI = 2
WHERE MANV = 'NV0002' AND MACA IN ('CA2400','CA2401','CA2402')

 /*TEST: TRƯỜNG HỢP 2 */

INSERT INTO LICHLAM (MANV, MACA, TRANGTHAI)
VALUES 
('NV0004', 'CA0014', NULL)

UPDATE LICHLAM 
SET TRANGTHAI = 0
WHERE MANV = 'NV0004' AND MACA ='CA0014'

/*TEST TRUÒNG HỢP 1 */

INSERT INTO LICHLAM (MANV, MACA, TRANGTHAI)
VALUES 
('NV0004', 'CA0013', NULL)





select * from lichlam
select*from chamcong
---------------------------------------------------------------------------------------------------------------------------
/*Trigger tự động thêm số giờ làm cho ca làm  . Nếu ca làm việc diễn ra 
vào ngày lễ (30/4-1/5 và 2/9) , tết dương lịch (1/1) thì số giờ làm việc nhân 3. 
Nếu không thì số giờ làm việc để bình thường.

Mô tả: Khi quản lý thêm ca làm cho nhân viên.Tự động cập nhật số giờ làm việc = giờ kết thúc - giờ tan làm  
Nếu ca làm có ngày làm vệc vào ngày lễ (cụ thể : 30/4-1/4 , 2/9) và ngày tết dương lịch (1/1) thì số giờ làm việc sẽ 
tăng gấp 3 lần so với số giờ bình thường. Mục đích của việc này là để đến cuối tháng
khi tính lương , nhân viên nào làm việc vào ngày lễ-tết sẽ được nhân 3 số lương bình thường.

Bảng : CALAM
Loại: After
Sự kiện: Insert
Process: 
1.Lấy @MACA , @GIO_BATDAU, @GIO_KETTHUC, @NGAY_LV từ bảng inserted
2.Tìm số giờ làm việc -> @SOGIOLAMVIEC = DATEDIFF(HOUR, @GIO_BATDAU, @GIO_KETTHUC)
3.Nếu ca làm có ngày làm việc vào ngày 30/4 , 1/5 ,2/9 và 1/1 
 ----> Cập nhật bảng CALAM: SOGIOLAMVIEC = @SOGIOLAMVIEC * 3 
				Điều kiện : MACA=@MACA
4.Nếu không: 
 ----> Cập nhật bảng CALAM: SOGIOLAMVIEC = @SOGIOLAMVIEC
				Điều kiện : MACA=@MACA */

CREATE TRIGGER trg_TangLuong
ON CALAM
AFTER INSERT
AS
BEGIN
    DECLARE @MACA VARCHAR(10), @SOGIOLAMVIEC INT, @NGAY_LV DATE,
            @GIO_BATDAU TIME, @GIO_KETTHUC TIME;

    
    SELECT @MACA = MACA, @GIO_BATDAU = GIO_BATDAU, 
           @GIO_KETTHUC = GIO_KETTHUC, @NGAY_LV = NGAY_LV
    FROM inserted;

    
    SET @SOGIOLAMVIEC = DATEDIFF(HOUR, @GIO_BATDAU, @GIO_KETTHUC)

    
    IF (MONTH(@NGAY_LV) = 4 AND DAY(@NGAY_LV) = 30) OR
       (MONTH(@NGAY_LV) = 5 AND DAY(@NGAY_LV) = 1) OR
       (MONTH(@NGAY_LV) = 9 AND DAY(@NGAY_LV) = 2) OR
       (MONTH(@NGAY_LV) = 1 AND DAY(@NGAY_LV) = 1)
    BEGIN

        
        UPDATE CALAM
        SET SOGIOLAMVIEC = @SOGIOLAMVIEC * 3
        WHERE MACA = @MACA
    END
	ELSE 
	BEGIN 
		UPDATE CALAM 
		SET SOGIOLAMVIEC = @SOGIOLAMVIEC
		WHERE MACA = @MACA
	END 
END


select * from calam
---TEST trường hợp ca làm vào ngày lễ 
insert into calam(MACA,NGAY_LV,BUOI_LAM,GIO_BATDAU,GIO_KETTHUC)
	values ('CA1216','2024-1-1',N'Sáng','08:00:00','09:00:00')
---TEST trường hợp ca làm vào ngày bình thường
insert into calam(MACA,NGAY_LV,BUOI_LAM,GIO_BATDAU,GIO_KETTHUC)
	values ('CA1217','2024-2-1',N'Sáng','08:00:00','09:00:00')

---------------------------------------------------------------------------------------------------------------------------
/* Module 3: Khi thông tin nhân viên mới được thêm vào , hệ thống tự động cập nhật thông tin nhân viên đó vào cơ sở dữ liệu.
	Mô tả: Module này tự động cập nhật thông tin nhân viên vào cơ sở dữ liệu sau khi nhân viên nhập thông tin. 
	-- Bảng: NHANVIEN
	-- Loại: After
	-- Sự kiện: Insert
	-- Process:
			1. Lấy @MANV,@NGAYSINH, @SDT, @CCCD, @CHUCVU, @TENNV, @DIACHI từ inserted
			2. Nếu số điện thoại có độ dài không hợp lệ (nhỏ hơn 10 hoặc lớn hơn 12 ký tự), trigger sẽ phát sinh lỗi (RAISERROR) và thực hiện ROLLBACK, hủy bỏ giao dịch.
			3. Kiểm tra địa chỉ: Nếu địa chỉ bị thiếu (giá trị NULL), trigger sẽ báo lỗi và hủy giao dịch.
			4. Kiểm tra chức vụ: Nếu không có chức vụ (giá trị NULL), trigger báo lỗi và hủy giao dịch
			5. Kiểm tra tên nhân viên: Nếu không có tên (giá trị NULL), trigger báo lỗi và hủy giao dịch.
			6. Kiểm tra căn cước công dân: Nếu căn cước đã tồn tại, trigger báo lỗi và hủy giao dịch.
			7. Kiểm tra số điện thoại: Nếu số điện thoại đã tồn tại, trigger báo lỗi và hủy giao dịch.
*/
CREATE OR ALTER TRIGGER tr_capnhatdl
ON NHANVIEN
AFTER INSERT
AS
	DECLARE @MANV VARCHAR(10);
    DECLARE @NGAYSINH DATE;
	DECLARE @SDT VARCHAR(15);  
    DECLARE @CCCD VARCHAR(50);
	DECLARE @CHUCVU NVARCHAR(50);
	DECLARE @TENNV NVARCHAR(255);
	DECLARE @DIACHI NVARCHAR(255);
BEGIN
-- Lấy thông tin từ bản ghi mới chèn
    SELECT @MANV = inserted.MANV,
       @NGAYSINH = inserted.NGAYSINH,
       @SDT = inserted.SDT,
       @CCCD = inserted.CCCD,
	   @CHUCVU = inserted.CHUCVU,
	   @TENNV = inserted.TENNV,
	   @DIACHI = inserted.DIACHI
	FROM inserted;
    -- Kiểm tra các điều kiện cần thiết
    IF LEN(@SDT)<10  OR LEN(@SDT)>12
    BEGIN
        RAISERROR('Thông tin nhập không hợp lệ, mời nhập lại', 16, 1);
        ROLLBACK; -- Hoặc xử lý khác tùy ý
    END
	IF @DIACHI IS NULL
	BEGIN
        RAISERROR('Thông tin nhập không hợp lệ, mời nhập lại', 16, 1);
        ROLLBACK; -- Hoặc xử lý khác tùy ý
    END
	IF @CHUCVU IS NULL
	BEGIN
        RAISERROR('Thông tin nhập không hợp lệ, mời nhập lại', 16, 1);
        ROLLBACK; -- Hoặc xử lý khác tùy ý
    END
	IF @TENNV IS NULL
	BEGIN
        RAISERROR('Thông tin nhập không hợp lệ, mời nhập lại', 16, 1);
        ROLLBACK; -- Hoặc xử lý khác tùy ý
    END
	IF EXISTS (SELECT 1 FROM NHANVIEN WHERE CCCD = @CCCD)
	BEGIN
		RAISERROR('Thông tin nhập không hợp lệ, mời nhập lại', 16, 1);
		ROLLBACK; -- Hoặc xử lý khác tùy ý
	END
	IF EXISTS (SELECT 1 FROM NHANVIEN WHERE SDT = @SDT)
	BEGIN
		RAISERROR('Thông tin nhập không hợp lệ, mời nhập lại', 16, 1);
		ROLLBACK; -- Hoặc xử lý khác tùy ý
	END

    -- Các hành động khác nếu cần
    -- Ví dụ: ghi nhật ký, cập nhật bảng khác, v.v.
	UPDATE NHANVIEN
    SET NGAYSINH = @NGAYSINH,
        SDT = @SDT,
        CCCD = @CCCD,
	    CHUCVU = @CHUCVU,
	    TENNV = @TENNV,
	    DIACHI = @DIACHI
    WHERE MANV = @MANV;
END;

INSERT INTO NHANVIEN (MANV, NGAYSINH, SDT, CCCD, CHUCVU, TENNV, DIACHI)
VALUES ('NV001', '1990-01-01', '0123456789', 'CCCD001', N'Pha chế', N'Nguyễn Văn A', N'Hà Nội');
---------------------------------------------------------------------------------------------------------------------------

/*Module 4: TModule 4: Trigger tự động random mật khẩu tài khoản cho nhân viên khi quản lý thêm một mã nhân viên mới trong tài khoản.
Mô tả : Nếu quản lý thêm một mã nhân viên mới với mục đích tạo tài khoản cho nhân viên  đó.
Hệ thống tự động random mật khẩu ban đầu cho nhân viên
-- Bảng: TAIKHOAN 
-- Sự kiện : After 
-- Loại : Insert 
-- Process : 
		1.Lấy @Manv từ bảng insterted 
		2.Dùng biến RAND()*10000 để lấy giá trị random mật khẩu từ 0 đến 9999 
		3.Chuyển giá trị random đó sang số nguyên với 4 chữ số
		4.Mật khẩu được thêm với điều kiện :  @MATKHAU='MK'+ 4 chữ số random phía sau. Nếu sau khi random mật khẩu ra theo cấu trúc vậy thì update thông tin bảng TAIKHOAN

*/

CREATE OR ALTER TRIGGER trg_RandomPassword
ON TAIKHOAN
AFTER INSERT
AS
BEGIN
    DECLARE @MANV VARCHAR(10), 
            @MATKHAU VARCHAR(225)

    
    SELECT @MANV = MANV 
    FROM INSERTED

    
    SET @MATKHAU = 'MK' + RIGHT('0000' + CAST(CAST(RAND() * 10000 AS INT) AS VARCHAR(4)), 4)

    
    UPDATE TAIKHOAN
    SET MATKHAU = @MATKHAU
    WHERE MANV = @MANV
END

INSERT INTO NHANVIEN (MANV, TENNV, NGAYSINH, DIACHI, SDT, CCCD, CHUCVU)
VALUES ('NV2005', 'Nguyen Van A', '1990-01-01', '123 Street', '0123475679', 'CCCD123755', 'Chuc Vu 1');

select *
fROM TAIKHOAN, NHANVIEN
WHERE MANV = 'NV2005'

ALTER TABLE TAIKHOAN 
ENABLE TRIGGER ALL
---------------------------------------------------------------------------------------------------------------------------

/*Module 5: Trigger tự động cập nhật thông tin nhân viên khi thông tin nhân viên mới được thêm vào.
	* Mô tả: Module này tự động cập nhật thông tin nhân viên vào cơ sở dữ liệu sau khi nhân viên nhập thông tin. 
		-- Bảng: NHANVIEN
		-- Loại: After
		-- Sự kiện: Insert
		-- Process:
			Nhập Thông tin: Nhân viên điền thông tin vào web.
			Xác thực Dữ liệu: Hệ thống kiểm tra tính hợp lệ của thông tin (định dạng số điện thoại, trường bắt buộc).
			Nếu có lỗi, hệ thống thông báo cho người dùng “Thông tin nhập không hợp lệ, mời nhập lại” để  nhân viên chỉnh sửa.
			Nếu số điện thoại <10 hoặc >12, hiển thị thông báo lỗi và rollback
			Nếu địa chỉ, chức vụ, tên nhân viên mà null, hiển thị thông báo lỗi và rollback.
			Nếu CCCD và SDT bị trùng, hiển thị thông báo lỗi và null.
			Nếu thông tin hợp lệ, hệ thống sẽ cập nhật tự động vào cơ sở dữ liệu.
*/
CREATE OR ALTER PROCEDURE DangKyCaLam
    @MaNhanVien VARCHAR(10),
    @MaCa VARCHAR(10),
    @ChucVu NVARCHAR(50)
AS
BEGIN
    DECLARE @SoPhucVu INT, @SoPhaChe INT;

    -- Kiểm tra xem nhân viên đã đăng ký ca này chưa
    IF EXISTS (SELECT 1 FROM LICHLAM WHERE MANV = @MaNhanVien AND MACA = @MaCa)
    BEGIN
        PRINT 'Nhân viên đã đăng ký ca này, không thể đăng ký lại!';
        RETURN;
    END

    -- Kiểm tra số lượng nhân viên phục vụ đã đăng ký trong ca
    SELECT @SoPhucVu = COUNT(*)
    FROM LICHLAM LL
    JOIN NHANVIEN NV ON LL.MANV = NV.MANV
    WHERE LL.MACA = @MaCa AND NV.CHUCVU = N'Phuc Vu';

    -- Kiểm tra số lượng nhân viên pha chế đã đăng ký trong ca
    SELECT @SoPhaChe = COUNT(*)
    FROM LICHLAM LL
    JOIN NHANVIEN NV ON LL.MANV = NV.MANV
    WHERE LL.MACA = @MaCa AND NV.CHUCVU = N'Pha Che';

    -- Kiểm tra điều kiện trước khi đăng ký
    IF @ChucVu = N'Pha Chế' AND @SoPhaChe >= 1
    BEGIN
        PRINT 'Ca này đã đủ Pha Chế, không thể đăng ký thêm!';
    END
    ELSE IF @ChucVu = N'Phục Vụ' AND @SoPhucVu >= 2
    BEGIN
        PRINT 'Ca này đã đủ Phục Vụ, không thể đăng ký thêm!';
    END
    ELSE
    BEGIN
        -- Thêm nhân viên vào lịch làm việc
        INSERT INTO LICHLAM (MANV, MACA)
        VALUES (@MaNhanVien, @MaCa);
        PRINT 'Đăng ký ca làm thành công!';
    END
END;

-- Thử đăng ký cho nhân viên NV01 vào ca CA002 (Phục Vụ)
EXEC DangKyCaLam 'NV0002', 'CA0002', N'Phuc Vu';
EXEC DangKyCaLam 'NV0002', 'CA0002', N'Phuc Vu';
EXEC DangKyCaLam 'NV0003', 'CA0002', N'Phuc Vu';
EXEC DangKyCaLam 'NV0004', 'CA0002', N'Phuc Vu';

-- Kiểm tra dữ liệu
SELECT * FROM NHANVIEN;
SELECT * FROM LICHLAM;

---------------------------------------------------------------------------------------------------------------------------

/*  Module 6: Giới hạn số ca làm việc của nhân viên
Mô tả: Giới hạn số ca làm việc của nhân viên không vượt quá 15 ca/ 1 tuần . 
	Nếu nhân viên đã đạt đến số lượng ca tối đa (15 ca trong một tuần),thủ tục sẽ thông báo để ngăn họ đăng ký thêm.
	Ngược lại, nếu số ca làm còn nằm trong giới hạn, nhân viên sẽ được phép tiếp tục đăng ký ca làm.
-- input: @MaNhanVien, @NgayHienTai: 
-- output:
			Nếu số ca làm trong tuần đã đạt 15: "Bạn đã đăng kí đủ số ca trong tuần. 
			Vui lòng không đăng ký thêm đến khi qua tuần tiếp theo !"
			Nếu số ca làm còn dưới 15: "Bạn có thể tiếp tục đăng ký ca làm việc."
-- process:
		1. Tính số ca làm việc trong 7 ngày gần nhất của nhân viên từ bảng LICHLAM 
		2. Kiểm tra số ca làm việc có vượt quá giới hạn
			+ Nếu số ca lớn hơn hoặc bằng 15: In ra thông báo "Bạn đã đăng kí đủ số ca trong tuần.
			Vui lòng không đăng ký thêm đến khi qua tuần tiếp theo!".
			+ Nếu số ca ít hơn 15: In ra thông báo "Bạn có thể tiếp tục đăng ký ca làm việc.".
*/

CREATE OR ALTER PROCEDURE KiemTraCaLamTrongTuan
    @MaNhanVien VARCHAR(10),
    @NgayHienTai DATE
AS
BEGIN
    DECLARE @SoCaLamTrongTuan INT;

    -- Tính số ca làm của nhân viên trong 7 ngày gần nhất
    SELECT @SoCaLamTrongTuan = COUNT(*)
    FROM LICHLAM LL
    JOIN CALAM CL ON LL.MACA = CL.MACA
    WHERE LL.MANV = @MaNhanVien
      AND CL.NGAY_LV BETWEEN DATEADD(DAY, -7, @NgayHienTai) AND @NgayHienTai;

    -- Kiểm tra nếu số ca làm vượt quá 15 ca
    IF @SoCaLamTrongTuan >= 15
    BEGIN
        PRINT N'Bạn đã đăng kí đủ số ca trong tuần. Vui lòng không đăng ký thêm đến khi qua tuần tiếp theo !';
    END
    ELSE
    BEGIN
        PRINT N'Bạn có thể tiếp tục đăng ký ca làm việc.';
    END
END;

--test
-- Giả sử hôm nay là ngày 2024-10-20 và kiểm tra cho nhân viên NV01
EXEC KiemTraCaLamTrongTuan 'NV0002', '2024-10-20';
SELECT * FROM CALAM;
SELECT * FROM LICHLAM;
SELECT * FROM NHANVIEN;

---------------------------------------------------------------------------------------------------------------------------

/* 
Module 7: Trigger tự động tính toán và cập nhật tổng lương (TONGLUONG) cho nhân viên
* Mô tả:  Trigger sẽ tự động tính tổng lương cho nhân viên dựa trên dữ liệu từ bảng CHAMCONG và cập nhật vào bảng BANGLUONG. 
		  Dữ liệu đầu vào gồm mã nhân viên, tổng số giờ làm việc, chức vụ, thưởng, và phạt. 
		  Dựa vào chức vụ để biết lương cơ bản của nhân viên.
		  Tổng lương sẽ được tính dựa vào công thức sau: TONGLUONG = LUONGCOBAN * TONGGIOLAM + THUONG/PHAT
		  Trigger sẽ thực hiện vào các sự kiện INSERT và UPDATE trên bảng CHAMCONG
	-- Trigger
	-- Bảng: CHAMCONG
	-- loại: after
	-- sự kiện: insert, update
	-- Process:
		1. Trigger lấy các thông tin về MANV, CHAMCONG_DATE, TONGSOGIOLAM, THUONG,PHAT từ bảng inserted
		2. Tính tổng số giờ làm trong tháng của nhân viên
		3. Cập nhật TONGSOGIO vào bảng CHAMCONG
		2. Xác định CHUCVU của nhân viên từ bảng NHANVIEN. Dựa vào CHUCVU, trigger sẽ tự động cập nhập lương cơ bản cho nhân viên:
			Nếu là phục vụ: lương cơ bản là 20,000.
			Nếu là quản lý: lương cơ bản là 25,000.
		3.  Trigger sẽ tự động tính TONGLUONG bằng công thức sau: 
			TONGLUONG = LUONGCOBAN * TONGGIOLAM + THUONG - ABS(PHAT)

		4. Cập nhật TONGLUONG trong bảng BANGLUONG.

*/

CREATE OR ALTER TRIGGER TRG_TINHTONGLUONG1
ON CHAMCONG
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @MANV VARCHAR(10),
            @CHAMCONG_DATE DATE,
            @THUONG DECIMAL(18, 2),
            @PHAT DECIMAL(18, 2),
            @CHUCVU NVARCHAR(50),
            @LUONGCOBAN DECIMAL(18, 2),
            @TONGLUONG DECIMAL(18, 2),
            @SOGIOLAM INT,
            @YEAR INT,
            @MONTH INT;

    SELECT TOP 1 
        @MANV = i.MANV,
        @CHAMCONG_DATE = i.CHAMCONG_DATE,
        @THUONG = ISNULL(i.THUONG, 0),
        @PHAT = ISNULL(i.PHAT, 0)
    FROM inserted i;

    SET @YEAR = YEAR(@CHAMCONG_DATE);
    SET @MONTH = MONTH(@CHAMCONG_DATE);

    SELECT @SOGIOLAM = SUM(c.SOGIOLAMVIEC)
    FROM CALAM c
    JOIN CHAMCONG cc ON cc.MANV = @MANV
    WHERE cc.MANV = @MANV
      AND YEAR(cc.CHAMCONG_DATE) = @YEAR
      AND MONTH(cc.CHAMCONG_DATE) = @MONTH;

    UPDATE CHAMCONG
    SET TONGSOGIOLAM = @SOGIOLAM
    WHERE MANV = @MANV
      AND YEAR(CHAMCONG_DATE) = @YEAR
      AND MONTH(CHAMCONG_DATE) = @MONTH;

    SELECT @CHUCVU = n.CHUCVU
    FROM NHANVIEN n
    WHERE n.MANV = @MANV;

    SET @LUONGCOBAN = CASE 
                        WHEN @CHUCVU = N'Phuc vu' THEN 20000 
                        WHEN @CHUCVU = N'Pha Che' THEN 25000 
                      END;

    -- Tính tổng lương
    SET @TONGLUONG = (@LUONGCOBAN * @SOGIOLAM) + @THUONG - ABS(@PHAT);

    -- Cập nhật hoặc thêm mới thông tin vào bảng BANGLUONG
    IF EXISTS (SELECT 1 FROM BANGLUONG WHERE MANV = @MANV)
    BEGIN
        UPDATE BANGLUONG
        SET TONGLUONG = @TONGLUONG, LUONGCOBAN = @LUONGCOBAN, BANGLUONG_DATE = GETDATE()
        WHERE MANV = @MANV;
    END
    ELSE
    BEGIN
        INSERT INTO BANGLUONG (MANV, TONGLUONG, LUONGCOBAN, BANGLUONG_DATE)
        VALUES (@MANV, @TONGLUONG, @LUONGCOBAN, GETDATE());
    END;
END;

INSERT INTO CHAMCONG (MANV, CHAMCONG_DATE, THUONG, PHAT)
VALUES ('NV0001', '2023-10-20', 50000, 0);

INSERT INTO CHAMCONG (MANV, CHAMCONG_DATE, THUONG, PHAT)
VALUES ('NV0002', '2023-10-20', 50000, 0);

SELECT * FROM BANGLUONG WHERE MANV = 'NV0001';

---------------------------------------------------------------------------------------------------------------------------

/*
Module 8: Tính phần thưởng và cập nhập tổng lương cho người làm việc hiệu quả nhất 
* Mô tả: Tính toán và cập nhật phần thưởng cho nhân viên có tổng số giờ làm việc cao nhất trong tháng. 
		 Tổng phần thưởng là 500,000 VNĐ, sẽ được chia đều cho số lượng nhân viên làm việc hiệu quả nhất, 
		 sau đó cập nhật vào cột TONGLUONG trong bảng BANGLUONG.
		
		-- Input: Không có đầu vào 
		-- Output: cập nhật bảng lương
		-- Process: 
			1. Thiết lập tổng tiền thưởng
			2. Tìm số giờ làm việc nhiều nhất trong tháng
			2. Đếm số người có số giờ làm việc cao nhất
			3. Chia phần thưởng và cập nhật tổng lương
			4. Cập nhật tổng lương của nhân viên trong bảng BANGLUONG
*/

CREATE OR ALTER PROCEDURE SP_THUONG_HIEUQUA
AS
BEGIN
    DECLARE @MAX_GIO INT,                -- Biến lưu số giờ làm việc nhiều nhất
            @SO_NGUOI INT,               -- Biến lưu số lượng nhân viên làm việc hiệu quả nhất
            @TONG_TIEN_THUONG DECIMAL(18, 2),  -- Tổng tiền thưởng
            @THUONG_MOI_NGUOI DECIMAL(18, 2);   -- Phần thưởng mỗi người

    SET @TONG_TIEN_THUONG = 500000;  -- Tổng tiền thưởng là 500,000 VNĐ

    -- Tìm số giờ làm việc nhiều nhất và số nhân viên có số giờ đó trong tháng hiện tại
    SELECT @MAX_GIO = MAX(TONGSOGIOLAM),
           @SO_NGUOI = COUNT(DISTINCT MANV)
    FROM CHAMCONG
    WHERE MONTH(CHAMCONG_DATE) = MONTH(GETDATE()) 
    AND YEAR(CHAMCONG_DATE) = YEAR(GETDATE());

    -- Kiểm tra nếu không có nhân viên nào trong tháng này (số lượng nhân viên = 0)
    IF @SO_NGUOI = 0
    BEGIN
        PRINT 'Không có nhân viên nào làm việc trong tháng này.';
        RETURN;  -- Thoát khỏi thủ tục nếu không có nhân viên nào
    END

    -- Tính phần thưởng mỗi người
    SET @THUONG_MOI_NGUOI = @TONG_TIEN_THUONG / @SO_NGUOI;

    -- Cập nhật tổng lương cho những nhân viên có số giờ làm việc cao nhất
    UPDATE BANGLUONG
    SET TONGLUONG = TONGLUONG + @THUONG_MOI_NGUOI
    WHERE MANV IN (
        SELECT MANV
        FROM CHAMCONG
        WHERE TONGSOGIOLAM = @MAX_GIO
        AND MONTH(CHAMCONG_DATE) = MONTH(GETDATE()) 
        AND YEAR(CHAMCONG_DATE) = YEAR(GETDATE())
    );

    -- Hiển thị thông tin của những nhân viên được thưởng
    SELECT BL.MANV, 
           BL.TONGLUONG - @THUONG_MOI_NGUOI AS TONGLUONG_TRUOC, -- Tổng lương trước khi cộng thưởng
           @THUONG_MOI_NGUOI AS THUONG_DUOC_CHIA, -- Phần thưởng được chia
           BL.TONGLUONG AS TONGLUONG_SAU -- Tổng lương sau khi cộng thưởng
    FROM BANGLUONG BL
    WHERE BL.MANV IN (
        SELECT MANV
        FROM CHAMCONG
        WHERE TONGSOGIOLAM = @MAX_GIO
        AND MONTH(CHAMCONG_DATE) = MONTH(GETDATE()) 
        AND YEAR(CHAMCONG_DATE) = YEAR(GETDATE())
    );
END;


EXEC SP_THUONG_HIEUQUA;

INSERT INTO BANGLUONG (MANV, BANGLUONG_DATE, LUONGCOBAN, TONGLUONG)
VALUES 
    ('NV000051', '2024-10-01', 5000000, 7000000),
    ('NV92', '2024-10-01', 6000000, 8500000),
    ('NV93', '2024-10-01', 5500000, 6000000),
    ('NV74', '2024-10-01', 5500000, 5500000);





---------------------------------------------------------------------------------------------------------------------------

﻿/*
Module 9: Tự động cập nhật tiền thưởng/phạt
	* Mô tả:  tự động cập nhật tiền thưởng/phạt dựa trên tongsogiolam trong bảng CHAMCONG biết rằng tiền lương là 20.000/giờ làm 
	và nếu làm đủ 100 giờ thì tiền thưởng sẽ = 20% tổng số giờ làm, nếu làm không đủ 100 giờ thì tiền phạt sẽ = 20% tổng số giờ làm
	-- Bảng: CHAMCONG
	-- loại: AFTER INSERT
	-- sự kiện: INSERT
	-- Process:
		1.Trigger này chạy khi có dữ liệu mới được thêm vào bảng CHAMCONG.
		2.Kiểm tra mã nhân viên và tổng số giờ làm việc trong bảng tạm inserted
		3.Lấy mốc 100 giờ làm làm mốc thưởng/phạt, nếu đủ 100 giờ làm thì lấy TONGSOGIOLAM nhân với LUONGCOBAN nhân với 0.2 (20%) rồi cập nhật dữ liệu vào cột THUONG
		nếu không đủ 100 giờ làm thì lấy TONGSOGIOLAM nhân với LUONGCOBAN nhân với 0.2 (20%) rồi cập nhật dữ liệu vào cột PHAT
*/
CREATE TRIGGER TR_TinhThuongPhat9
ON CHAMCONG
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @MANV VARCHAR(10);
    DECLARE @TONGSOGIOLAM INT;
    DECLARE @LUONGCOBAN DECIMAL(18, 2) = 20000; -- Lương cơ bản 20.000/giờ
    DECLARE @THUONG DECIMAL(18, 2) = 0;
    DECLARE @PHAT DECIMAL(18, 2) = 0;

    -- Lấy mã nhân viên và tổng số giờ làm việc từ bản ghi mới thêm vào
    SELECT @MANV = MANV, @TONGSOGIOLAM = TONGSOGIOLAM 
    FROM inserted;

    -- Nếu tổng số giờ làm >= 100 thì thưởng 20%
    IF @TONGSOGIOLAM >= 100
    BEGIN
        SET @THUONG = (@TONGSOGIOLAM * @LUONGCOBAN) * 0.2; -- 20% tiền thưởng
        SET @PHAT = 0;
    END
    -- Nếu tổng số giờ làm < 100 thì phạt 20%
    ELSE
    BEGIN
        SET @PHAT = (@TONGSOGIOLAM * @LUONGCOBAN) * 0.2; -- 20% tiền phạt
        SET @THUONG = 0;
    END

    -- Cập nhật cột THUONG và PHAT trong bảng CHAMCONG
    UPDATE CHAMCONG
    SET THUONG = @THUONG, PHAT = @PHAT
    WHERE MANV = @MANV AND CHAMCONG_DATE = (SELECT MAX(CHAMCONG_DATE) FROM CHAMCONG WHERE MANV = @MANV);
END;
--test:
INSERT INTO CHAMCONG (MANV, CHAMCONG_DATE, TONGSOGIOLAM)
VALUES ('NV1001', '2024-10-20', 105);

UPDATE CHAMCONG
SET TONGSOGIOLAM = 105
WHERE MANV = 'NV0001' AND CHAMCONG_DATE = '2024-10-19';

SELECT * FROM CHAMCONG WHERE MANV = 'NV1001';

---------------------------------------------------------------------------------------------------------------------------
/*
Module 10: Xóa thông tin nhân viên khi ngày làm việc gần nhất của nhân viên đó là 10 năm trở
lại đây.
	* Nếu ngày làm việc của nhân viên đó gần nhất là 10 năm trở lại thì hệ thống sẽ xóa
thông tin của nhân viên. Mục đích là để giảm tải dung lượng cho hệ thống.
	-- Bảng: NHANVIEN VÀ CÁC BẢNG CÒN LẠI
	-- loại: AFTER UPDATE
	-- sự kiện: UPDATE
	-- Process:
		-- Input: MANV
		-- Output: Thông tin nhân viên làm việc trên 10 năm bị xóa
		-- Process:
				1.Lấy ngày làm việc gần nhất của nhân viên khi nhân viên đăng kí ca làm trong bảng LICHLAM34
				2.Nếu ngày làm việc gần nhất là cách đây hơn 10 năm → Tiến hành xóa tất cả thông
				tin của nhân viên đó trong hệ thống..
*/
CREATE OR ALTER PROCEDURE SP_DeleteEmployee 
    @MANV VARCHAR(10)
AS
BEGIN
    DECLARE @LastWorkDate DATE;


    SELECT @LastWorkDate = MAX(NGAY_LV)
    FROM LICHLAM L 
    JOIN CALAM C ON L.MACA = C.MACA
    WHERE MANV = @MANV;


    IF @LastWorkDate <= DATEADD(YEAR, -10, GETDATE())
    BEGIN
        DELETE FROM CHAMCONG WHERE MANV = @MANV;
        DELETE FROM BANGLUONG WHERE MANV = @MANV;
        DELETE FROM LICHLAM WHERE MANV = @MANV;
        DELETE FROM TAIKHOAN WHERE MANV = @MANV;
        DELETE FROM NHANVIEN WHERE MANV = @MANV;
    END
END;

-- Test
INSERT INTO CALAM (MACA, NGAY_LV, BUOI_LAM, GIO_BATDAU, GIO_KETTHUC, SOGIOLAM)
VALUES ('CA2006', '2005-10-22', 'Sáng', '08:00:00', '12:00:00', 4);

INSERT INTO LICHLAM (MANV, MACA, TRANGTHAI)
VALUES ('NV2005', 'CA2006', '2');


DECLARE @a VARCHAR(10);
EXECUTE SP_DeleteEmployee 'NV2005';



