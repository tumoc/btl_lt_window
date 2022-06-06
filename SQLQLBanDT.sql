/*
Created		12/30/2021
Modified		01/10/2022
Project		
Model		
Company		
Author		
Version		
Database		MS SQL 7 
*/
use master
go
if(exists (select * from sysdatabases where  name = 'QLCuaHangDT'))
	drop database QLCuaHangDT
go
create database QLCuaHangDT
go
use QLCuaHangDT
go
use QLCuaHangDT

Create table [KhachHang] (
	[MaKH] Integer Identity(1,1) NOT NULL,
	[TenKH] Nvarchar(100) NULL,
	[DCKH] Nvarchar(100) NULL,
	[SoDTKH] Char(12) NULL,
	[EmailKH] Nchar(50) NULL,
Primary Key  ([MaKH])
) 
Create table [NhanVien] (
	[MaNV] Integer Identity(1,1) NOT NULL,
	[HoTenNV] Nvarchar(100) NULL,
	[SoDTNV] Char(12) NULL,
	[DiaChiNV] Nvarchar(100) NULL,
	[TaiKhoan] Char(20) NULL,
	[MatKhau] Char(20) NULL,
	[NgayVao] Datetime NULL,
	[ChucVu] Nvarchar(30) NULL,
Primary Key  ([MaNV])
) 
Create table [NhaCungCap] (
	[MaNCC] Integer Identity(1,1) NOT NULL,
	[TenNCC] Nvarchar(50) NULL,
	[SoDTNCC] Char(12) NULL,
	[DiaChiNCC] Nvarchar(50) NULL,
Primary Key  ([MaNCC])
) 
Create table [DienThoai] (
	[MaDT] Integer Identity(1,1) NOT NULL,
	[TenDT] Char(20) NULL,
	[GiaBan] Money NULL,
	[BaoHanh] Nvarchar(50) NULL,
Primary Key  ([MaDT])
) 
create table [HoaDon] (
	[MaHD] Integer Identity(1,1) NOT NULL,
	[MaKH] Integer NOT NULL,
	[NgayLap] Datetime NULL,
	[GiamGia] Integer NULL,
	[MaNV] Integer  NULL,
Primary Key  ([MaHD])
) 
Create table [KhoHang] (
	[MaNCC] Integer NOT NULL,
	[MaDT] Integer NOT NULL,
	[SoLuongCon] Integer NULL,
	[NgayNhap] Datetime NULL,
	[GiaNhap] Money NULL,
Primary Key  ([MaNCC],[MaDT])
) 
Create table [ChiTietHD] (
	[MaHD] Integer NOT NULL,
	[MaDT] Integer NOT NULL,
	[SoLuong] Integer NULL,
Primary Key  ([MaHD],[MaDT])
) 
go

Alter table [HoaDon] add  foreign key([MaKH]) references [KhachHang] ([MaKH]) 
go
Alter table [HoaDon] add  foreign key([MaNV]) references [NhanVien] ([MaNV])  on delete set null
go
Alter table [KhoHang] add  foreign key([MaNCC]) references [NhaCungCap] ([MaNCC]) 
go
Alter table [KhoHang] add  foreign key([MaDT]) references [DienThoai] ([MaDT]) 
go
Alter table [ChiTietHD] add  foreign key([MaDT]) references [DienThoai] ([MaDT]) 
go
Alter table [ChiTietHD] add  foreign key([MaHD]) references [HoaDon] ([MaHD]) 
go


go
insert into  NhaCungCap values
('FPT','0123 321 321',N'Hà Nội'),
(N'Hoàng Kiên','0123 321 333',N'Hà Nội'),
(N'Kho Sỉ Hà Nội','0123 321 444',N'Hà Nội'),
(N'Mobile City','0123 321 456',N'Hà Nội'),
(N'MS Mobile','0123 321 476',N'Hà Nội'),
(N'Mi Media','0123 321 555',N'Nam Định'),
(N'Di Động Số','0123 321 666',N'Hải Phòng'),
(N'Điện Thoại Việt','0123 777 555',N'Lạng Sơn'),
(N'Điện Thoại Hay','0123 777 555',N'Hà Nội'),
(N'Xiaomi Việt','0123 777 555',N'Lạng Sơn')
go
insert into DienThoai values
('SamSung S20', 18000000,N'12 tháng'),
('SamSung Note 20', 20000000,N'12 tháng'),
('SamSung S10 Ultra', 18000000,N'12 tháng'),
('Samsung A70', 6000000,N'12 tháng'),
('Apple IP X', 8000000,N'12 tháng'),
('Apple IP 11', 16000000,N'12 tháng'),
('Apple IP 12', 16000000,N'12 tháng'),
('Xiaomi Mi 11', 10000000,N'12 tháng'),
('ZTE X20', 12000000,N'12 tháng'),
('Samsung A52', 10000000,N'12 tháng'),
('Xiaomi Note 9s', 12000000,N'12 tháng')

go
insert into KhoHang values
(1,2,10,'2021/3/21', 1700000),
(2,1,20,'2021/4/20', 1700000),
(3,2,20,'2021/4/21',17000000),
(4,3,30,'2021/4/25',11000000),
(5,4,40,'2021/4/29',4000000),
(6,5,25,'2021/4/29',6000000),
(7,6,25,'2021/4/29',13000000),
(8,7,25,'2021/4/29',8000000),
(9,8,25,'2021/4/29',10000000),
(1,9,10,'2021/4/29',9000000)
go
insert into KhachHang values
(N'Việt Hoàng',N'Bắc Từ Liêm, Hà Nội','0123 321 111', 'tranviethoang@gamil.com'),
(N'Đức Khải',N'Bắc Từ Liêm, Hà Nội','0123 321 222', 'leduckhai@gamil.com'),
(N'Nguyễn Hiếu',N'Bắc Từ Liêm, Hà Nội','0123 321 333', 'hieu@gamil.com'),
(N'Trần Hiếu',N'Bắc Từ Liêm, Hà Nội','0123 321 444', 'hieu@gamil.com'),
(N'Trần Hoàng',N'Bắc Từ Liêm, Hà Nội','0123 321 333', 'hoang@gamil.com'),
(N'Harry',N'Bắc Từ Liêm, Hà Nội','0123 321 666', 'hary@gamil.com'),
(N'Trung Hòa',N'Bắc Từ Liêm, Hà Nội','0123 321 655', 'hoa@gamil.com'),
(N'Đức Thọ',N'Bắc Từ Liêm, Hà Nội','0123 321 665', 'tho@gamil.com'),
(N'Trung Kiên',N'Bắc Từ Liêm, Hà Nội','0123 321 765', 'kien@gamil.com'),
(N'Phạm Tùng',N'Bắc Từ Liêm, Hà Nội','0123 321 332', 'tung@gamil.com')
go
insert into NhanVien values
(N'Trần Văn Hoàn','0123 321 444',N'Ô Chợ Dừa, Đống Đa','hoantv','123@','2020/2/21',N'Nhân Viên'),
(N'Nuyễn Thị Hiền','0123 321 555',N'Nguyễn Trãi, Thanh Xuân','hiennt','123@','2020/3/21',N'Nhân Viên'),
(N'Lê Thị Thơm','0123 321 666',N'Nhổn, Bắc Từ Liêm','thomlt','123@','2020/2/21',N'Nhân Viên'),
(N'Lê Văn Anh','0123 321 654',N'Nhổn, Bắc Từ Liêm','anhlv','123@','2020/2/21',N'Nhân Viên'),
(N'Nguyễn Anh Tuấn','0123 432 666',N'Nhổn, Bắc Từ Liêm','tuanna','123@','2020/2/21',N'Nhân Viên'),
(N'Nguyễn Hà Nam','0123 321 433',N'Nhổn, Bắc Từ Liêm','namnh','123@','2020/2/21',N'Nhân Viên'),
(N'Trần Trung Đức','0123 321 888',N'Nhổn, Bắc Từ Liêm','ductt','123@','2020/2/21',N'Nhân Viên'),
(N'Lê Trung Kiên','0123 321 654',N'Nhổn, Bắc Từ Liêm','kienlt','123@','2020/2/21',N'Nhân Viên'),
(N'Lê Văn Lương','0123 321 466',N'Nhổn, Bắc Từ Liêm','luonglv','123@','2020/2/21',N'Quản Lý'),
(N'Trần Doanh Nhân','0123 321 876',N'Nhổn, Bắc Từ Liêm','admin','admin','2020/2/21',N'Quản Lý')
go
insert into HoaDon values
(1,'2021/04/20',0,NULL),
(1,'2021/04/20',0,1),
(2,'2021/04/21',10,2),
(3,'2021/04/22',0,1),
(4,'2021/04/20',0,3),
(5,'2021/04/20',0,4),
(6,'2021/04/20',0,5),
(7,'2021/04/20',0,6),
(8,'2021/04/20',0,7),
(9,'2021/04/20',0,8),
(9,'2021/04/20',0,9)
go
insert into ChiTietHD values
(1,2,1),
(1,1,1),
(2,2,1),
(3,3,3),
(4,4,4),
(5,5,5),
(6,6,1),
(7,7,2),
(8,8,1),
(9,9,1),
(10,6,1)

go
select *from KhoHang
select *from ChiTietHD
select *from DienThoai
select *from HoaDon
select *from KhachHang
select *from NhanVien
select *from NhaCungCap

go
create trigger TRG_KhoHang
on ChiTietHD
for insert
as
	begin
		declare @sl int
		declare @slBan int

		select @sl = SoLuongCon from KhoHang inner join inserted
		on KhoHang.MaDT = inserted.MaDT

		select @slBan = SoLuong from  inserted

		if(@slBan > @sl)
			begin	
				raiserror(N'Không đủ hàng',16,1)
				rollback tran
			end
		else
			update KhoHang set SoLuongCon = SoLuongCon - @slBan
			from KhoHang inner join inserted
			on KhoHang.MaDT = inserted.MaDT
	end
go
create trigger update_kho
on ChiTietHD
for update
as	
	begin
		declare @truoc int
		declare @sau int

		select @truoc = deleted.SoLuong
		from deleted

		select @sau = inserted.SoLuong
		from inserted

		update KhoHang set SoLuongCon = SoLuongCon - (@sau -@truoc)
		from KhoHang inner join inserted
		on KhoHang.MaDT = inserted.MaDT
		inner join deleted
		on KhoHang.MaDT = deleted.MaDT
	end


go
drop table ChiTietHD
drop table KhoHang
drop table DienThoai
drop table HoaDon
drop table KhachHang
drop table NhanVien
drop table NhaCungCap









