🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI
Buổi 5 – Tiết 1: Kiến trúc ADO.NET căn bản và các thành phần cốt lõi
1. Mục tiêu và bối cảnh thực tế
Tiết thực hành này tập trung tìm hiểu ADO.NET và cách ứng dụng C# làm việc với cơ sở dữ liệu SQL Server. Nội dung được triển khai thông qua các công việc kết nối cơ sở dữ liệu, truy vấn thông tin sản phẩm, cập nhật số lượng tồn kho và đọc danh sách hàng hóa.
Mục tiêu
- Hiểu được vai trò của ADO.NET trong các ứng dụng .NET có sử dụng cơ sở dữ liệu.
- Nắm được nhiệm vụ của những thành phần chính như SqlConnection, SqlCommand, SqlParameter, SqlDataReader, DataTable và SqlDataAdapter.
- Phân biệt được các phương thức thực thi truy vấn và biết lựa chọn phương thức phù hợp với từng loại công việc.
- Biết cách truyền tham số vào câu lệnh SQL để xử lý dữ liệu an toàn hơn.
- Nhận biết nguy cơ SQL Injection khi xây dựng câu truy vấn từ dữ liệu đầu vào.
- Thực hành các thao tác dữ liệu cơ bản phục vụ cho hệ thống quản lý siêu thị mini.
Bối cảnh thực tế
Trong hoạt động của một siêu thị mini, hệ thống cần lưu trữ và tra cứu thông tin hàng hóa, kiểm tra số lượng sản phẩm hiện có, cập nhật tồn kho sau các nghiệp vụ phát sinh và hiển thị danh sách sản phẩm để nhân viên theo dõi. Những thao tác này yêu cầu ứng dụng phải giao tiếp được với cơ sở dữ liệu một cách ổn định và an toàn.

ADO.NET là công nghệ hỗ trợ chương trình C# thực hiện việc giao tiếp đó với SQL Server. Việc tìm hiểu các thành phần của ADO.NET giúp xây dựng nền tảng cho những chức năng quản lý sản phẩm, hàng tồn kho và bán hàng trong đề tài Mini Supermarket.
2. Tìm hiểu kiến trúc ADO.NET
ADO.NET là bộ công nghệ truy xuất dữ liệu trong nền tảng .NET, cho phép ứng dụng kết nối với hệ quản trị cơ sở dữ liệu, gửi yêu cầu truy vấn và nhận kết quả để xử lý. Trong buổi thực hành, nội dung tập trung vào cách ADO.NET hỗ trợ ứng dụng C# làm việc với Microsoft SQL Server.
Các thành phần chính được tìm hiểu
- SqlConnection: Thành phần chịu trách nhiệm tạo và quản lý kết nối giữa ứng dụng với SQL Server. Kết nối cần được mở trước khi thực hiện các thao tác dữ liệu và giải phóng sau khi hoàn thành.
- SqlCommand: Thành phần dùng để gửi câu lệnh SQL hoặc yêu cầu thực thi đến cơ sở dữ liệu.
- SqlParameter: Thành phần dùng để truyền các giá trị vào câu lệnh SQL thông qua tham số, giúp xử lý dữ liệu đầu vào rõ ràng và hạn chế nguy cơ SQL Injection.
- SqlDataReader: Thành phần hỗ trợ đọc dữ liệu tuần tự từng dòng từ kết quả truy vấn khi kết nối đang mở.
- DataTable: Cấu trúc lưu dữ liệu theo dạng bảng trong bộ nhớ để ứng dụng có thể tiếp tục sử dụng dữ liệu sau khi hoàn tất việc lấy dữ liệu.
- SqlDataAdapter: Thành phần hỗ trợ lấy dữ liệu từ nguồn dữ liệu và nạp vào các cấu trúc như DataTable hoặc DataSet.
Quy trình làm việc với cơ sở dữ liệu
Trong quá trình thực hành, quy trình làm việc với ADO.NET được tìm hiểu theo các bước chính:
1. Xác định thông tin kết nối đến SQL Server.
2. Tạo và mở kết nối đến cơ sở dữ liệu.
3. Chuẩn bị yêu cầu truy vấn hoặc cập nhật dữ liệu.
4. Truyền các tham số cần thiết nếu câu lệnh có sử dụng tham số.
5. Chọn phương thức thực thi phù hợp với mục tiêu của thao tác.
6. Tiếp nhận và xử lý kết quả trả về.
7. Đóng và giải phóng tài nguyên sau khi hoàn tất công việc.
Quy trình trên giúp tổ chức thao tác dữ liệu có trình tự, đồng thời hạn chế việc giữ kết nối không cần thiết và hỗ trợ xử lý lỗi trong quá trình làm việc.
3. Công nghệ và công cụ sử dụng
Các công nghệ và công cụ được sử dụng hoặc đề cập trong buổi thực hành gồm:
- C#: Ngôn ngữ lập trình được dùng để xây dựng chương trình thực hành.
- .NET: Nền tảng chạy ứng dụng và cung cấp các thư viện cần thiết.
- ADO.NET: Công nghệ truy xuất dữ liệu giữa ứng dụng và cơ sở dữ liệu.
- Microsoft.Data.SqlClient: Thư viện hỗ trợ kết nối và thao tác với SQL Server.
- Microsoft SQL Server: Hệ quản trị cơ sở dữ liệu lưu trữ thông tin của hệ thống.
- Visual Studio: Môi trường phát triển, xây dựng và chạy chương trình.
- MiniSupermarketDb: Cơ sở dữ liệu sử dụng trong nội dung thực hành.
- Bảng Products: Bảng dữ liệu sản phẩm được dùng để minh họa các thao tác truy vấn và cập nhật.
4. Tìm hiểu kết nối cơ sở dữ liệu
Một trong những công việc đầu tiên là tìm hiểu cách cấu hình kết nối từ chương trình C# đến SQL Server. Chuỗi kết nối cần xác định chính xác máy chủ, cơ sở dữ liệu và phương thức xác thực được sử dụng trên máy thực hành.

Trong nội dung này, cơ sở dữ liệu được sử dụng là MiniSupermarketDb. Khi triển khai trên máy khác, thông tin máy chủ có thể cần điều chỉnh tùy theo cấu hình SQL Server đang cài đặt.
Các công việc đã tìm hiểu
- Xác định tên máy chủ SQL Server đang sử dụng.
- Xác định tên cơ sở dữ liệu cần truy cập.
- Kiểm tra phương thức xác thực kết nối.
- Kiểm tra dịch vụ SQL Server có đang hoạt động hay không.
- Tìm hiểu việc mở kết nối trước khi gửi yêu cầu truy vấn.
- Tìm hiểu cách quản lý và giải phóng kết nối sau khi hoàn thành thao tác.
- Nhận biết các nguyên nhân phổ biến khiến ứng dụng không kết nối được với cơ sở dữ liệu.
Việc cấu hình kết nối chính xác là điều kiện cần để các thao tác đọc, thêm, sửa hoặc xóa dữ liệu được thực hiện thành công.
5. Phân biệt các phương thức thực thi câu lệnh SQL
Trong buổi học, ba phương thức thực thi thường dùng được tìm hiểu và phân biệt theo mục đích sử dụng.
ExecuteScalar
Phương thức này phù hợp khi chương trình chỉ cần nhận về một giá trị đơn từ kết quả truy vấn. Trong bài thực hành, phương thức được dùng để lấy tổng số sản phẩm trong bảng Products.
ExecuteNonQuery
Phương thức này phù hợp với những thao tác không nhằm trả về danh sách dữ liệu, chẳng hạn như thêm, cập nhật hoặc xóa bản ghi. Bài thực hành sử dụng phương thức này để cập nhật số lượng tồn kho của một sản phẩm và kiểm tra số dòng dữ liệu bị ảnh hưởng.
ExecuteReader
Phương thức này được sử dụng khi cần đọc một hoặc nhiều dòng dữ liệu từ kết quả truy vấn. Trong bài thực hành, phương thức hỗ trợ lấy danh sách sản phẩm và đọc lần lượt các thông tin như mã sản phẩm, mã vạch, tên sản phẩm, đơn giá và số lượng tồn kho.
Kết quả tìm hiểu
Qua nội dung này, sinh viên phân biệt được mục đích của từng phương thức và hiểu rằng việc lựa chọn phương thức phù hợp giúp chương trình xử lý kết quả truy vấn đúng với yêu cầu nghiệp vụ.
6. Tìm hiểu an toàn truy vấn và SQL Injection
SQL Injection là nguy cơ bảo mật có thể xảy ra khi dữ liệu đầu vào được ghép trực tiếp vào câu lệnh SQL. Trong trường hợp dữ liệu không được xử lý đúng cách, nội dung đầu vào có thể làm thay đổi ý nghĩa của câu truy vấn.
Nội dung đã tìm hiểu
- Nhận biết nguy cơ khi nối trực tiếp dữ liệu do người dùng cung cấp vào câu lệnh SQL.
- Tìm hiểu cách truyền giá trị đầu vào bằng tham số SQL.
- Hiểu vai trò của SqlParameter trong việc truyền dữ liệu vào câu truy vấn.
- Tìm hiểu cách sử dụng tham số để hạn chế việc dữ liệu đầu vào bị diễn giải thành một phần của câu lệnh SQL.
- Nhận thức được tầm quan trọng của việc xây dựng truy vấn an toàn khi ứng dụng làm việc với dữ liệu người dùng.
Sử dụng truy vấn có tham số là một biện pháp quan trọng giúp giảm nguy cơ SQL Injection. Đây là kiến thức cần thiết khi phát triển các chức năng quản lý sản phẩm, tài khoản, đơn hàng và những dữ liệu khác trong hệ thống.
7. Nội dung thực hành
Bài tập 1: Đếm tổng số mặt hàng
Mục tiêu: Tìm hiểu cách lấy một giá trị thống kê từ cơ sở dữ liệu bằng phương thức ExecuteScalar().

Công việc thực hiện:
- Kết nối chương trình C# với cơ sở dữ liệu MiniSupermarketDb.
- Thực hiện truy vấn lấy tổng số bản ghi trong bảng Products.
- Nhận giá trị trả về và chuyển đổi sang kiểu dữ liệu phù hợp để hiển thị.
- Thông báo trạng thái kết nối và kết quả thống kê trên cửa sổ Console.
- Tìm hiểu cách xử lý lỗi có thể xảy ra trong quá trình kết nối hoặc truy vấn.
Kết quả mong đợi: Chương trình thông báo kết nối thành công và hiển thị tổng số mặt hàng hiện có trong bảng Products. Tài liệu thực hành minh họa kết quả là 20 sản phẩm; số lượng thực tế phụ thuộc dữ liệu đang có trong cơ sở dữ liệu.

Ý nghĩa: Thao tác thống kê số lượng mặt hàng là ví dụ cơ bản cho các chức năng báo cáo trong hệ thống siêu thị mini, chẳng hạn như thống kê tổng số sản phẩm đang được quản lý.
Bài tập 2: Cập nhật số lượng tồn kho
Mục tiêu: Tìm hiểu cách cập nhật thông tin sản phẩm bằng phương thức ExecuteNonQuery() kết hợp với tham số SQL.

Công việc thực hiện:
- Xác định sản phẩm cần cập nhật thông qua mã sản phẩm.
- Xác định số lượng tồn kho mới cần lưu.
- Chuẩn bị yêu cầu cập nhật dữ liệu trong bảng Products.
- Truyền mã sản phẩm và số lượng mới bằng các tham số SQL.
- Thực hiện thao tác cập nhật và kiểm tra số dòng dữ liệu bị ảnh hưởng.
- Thông báo kết quả thành công hoặc cảnh báo khi không tìm thấy sản phẩm phù hợp.
Kết quả mong đợi: Nếu sản phẩm có mã tương ứng tồn tại, số lượng tồn kho được cập nhật theo giá trị đã xác định. Chương trình thông báo số dòng dữ liệu bị ảnh hưởng; nếu không có sản phẩm phù hợp, chương trình hiển thị cảnh báo.

Ý nghĩa: Cập nhật tồn kho là nghiệp vụ quan trọng trong hệ thống quản lý siêu thị. Số lượng hàng hóa cần được theo dõi và điều chỉnh chính xác để hỗ trợ quản lý kho và phục vụ hoạt động bán hàng.
Bài tập 3: Đọc danh sách sản phẩm
Mục tiêu: Tìm hiểu cách lấy và đọc tuần tự nhiều bản ghi từ cơ sở dữ liệu bằng SqlDataReader.

Công việc thực hiện:
- Truy vấn các thông tin cần thiết từ bảng Products.
- Sắp xếp danh sách theo mã sản phẩm tăng dần.
- Đọc lần lượt các bản ghi trong kết quả truy vấn.
- Lấy thông tin mã sản phẩm, mã vạch, tên sản phẩm, đơn giá và số lượng tồn kho.
- Trình bày dữ liệu theo dạng bảng trên cửa sổ Console.
- Đếm số lượng bản ghi đã đọc và hiển thị tổng số sản phẩm được truy xuất.
- Tìm hiểu cách xử lý lỗi SQL trong quá trình đọc dữ liệu.
Kết quả mong đợi: Chương trình hiển thị danh sách sản phẩm theo thứ tự mã sản phẩm tăng dần, bao gồm mã sản phẩm, mã vạch, tên sản phẩm, đơn giá và tồn kho. Số dòng hiển thị phụ thuộc dữ liệu thực tế trong bảng.

Ý nghĩa: Chức năng đọc danh sách là nền tảng cho các màn hình quản lý hàng hóa, tra cứu sản phẩm và kiểm tra tồn kho trong hệ thống Mini Supermarket.
8. Quy trình chuẩn bị và chạy chương trình
Để thực hiện các nội dung thực hành, cần chuẩn bị môi trường lập trình và cơ sở dữ liệu theo các bước sau:
1. Mở Visual Studio và tạo hoặc mở dự án Console C# dùng cho bài thực hành.
2. Kiểm tra thư viện hỗ trợ kết nối SQL Server đã được cài đặt trong dự án.
3. Khởi động SQL Server và xác nhận dịch vụ cơ sở dữ liệu đang hoạt động.
4. Kiểm tra cơ sở dữ liệu MiniSupermarketDb đã tồn tại.
5. Kiểm tra bảng Products cùng các cột dữ liệu cần dùng trong bài thực hành.
6. Đối chiếu chuỗi kết nối với cấu hình SQL Server trên máy.
7. Chạy lần lượt các bài tập và quan sát kết quả hiển thị trên cửa sổ Console.
8. Kiểm tra dữ liệu trong cơ sở dữ liệu khi cần xác nhận thao tác cập nhật đã được thực hiện.
Các lỗi thường gặp và hướng kiểm tra
- Không kết nối được SQL Server: Kiểm tra tên máy chủ, trạng thái dịch vụ và phương thức xác thực.
- Không tìm thấy cơ sở dữ liệu: Kiểm tra tên MiniSupermarketDb và máy chủ đang kết nối.
- Không tìm thấy bảng hoặc cột: Đối chiếu cấu trúc bảng Products và tên các cột với cơ sở dữ liệu thực tế.
- Không cập nhật được sản phẩm: Kiểm tra mã sản phẩm được chọn có tồn tại hay không.
- Thiếu thư viện kết nối: Kiểm tra gói Microsoft.Data.SqlClient trong dự án.
- Kết quả truy vấn không như mong đợi: Kiểm tra dữ liệu hiện có, điều kiện truy vấn và tên trường dữ liệu được sử dụng.
9. Checklist tự đánh giá
Sau khi hoàn thành nội dung thực hành, có thể tự kiểm tra mức độ nắm vững kiến thức thông qua các tiêu chí sau:
- [ ] Giải thích được vai trò của ADO.NET trong ứng dụng .NET.
- [ ] Phân biệt được nhiệm vụ của SqlConnection và SqlCommand.
- [ ] Nêu được mục đích sử dụng của SqlParameter.
- [ ] Hiểu cách SqlDataReader đọc dữ liệu tuần tự.
- [ ] Biết vai trò cơ bản của DataTable và SqlDataAdapter.
- [ ] Phân biệt được ExecuteScalar(), ExecuteNonQuery() và ExecuteReader().
- [ ] Hiểu quy trình kết nối, truy vấn, xử lý kết quả và giải phóng tài nguyên.
- [ ] Giải thích được SQL Injection và lợi ích của truy vấn có tham số.
- [ ] Hoàn thành nội dung thống kê số lượng mặt hàng.
- [ ] Hoàn thành nội dung cập nhật số lượng tồn kho.
- [ ] Hoàn thành nội dung đọc và hiển thị danh sách sản phẩm.
- [ ] Biết kiểm tra các lỗi kết nối và lỗi truy vấn cơ bản.
10. Kết quả và kiến thức đạt được
Sau buổi thực hành, các nội dung kiến thức chính được hệ thống hóa như sau:
- Nắm được khái niệm và vai trò của ADO.NET trong việc truy xuất dữ liệu.
- Hiểu cách ứng dụng C# giao tiếp với SQL Server thông qua các thành phần cốt lõi.
- Biết lựa chọn phương thức thực thi dựa trên kết quả mà chương trình cần nhận.
- Hiểu cách truy vấn dữ liệu thống kê, cập nhật bản ghi và đọc danh sách dữ liệu.
- Nhận thức được tầm quan trọng của việc truyền tham số thay vì nối trực tiếp dữ liệu đầu vào vào câu lệnh SQL.
- Biết một số bước kiểm tra và xử lý các lỗi thường gặp khi kết nối hoặc thao tác với cơ sở dữ liệu.
- Có nền tảng để tiếp tục phát triển các chức năng quản lý sản phẩm và tồn kho trong đề tài Mini Supermarket.
11. Kết luận
Qua Buổi 5 – Tiết 1, sinh viên tìm hiểu kiến trúc ADO.NET và vai trò của các thành phần thường dùng khi ứng dụng C# làm việc với SQL Server. Nội dung thực hành tập trung vào ba nhóm công việc: thống kê tổng số mặt hàng, cập nhật số lượng tồn kho và đọc danh sách sản phẩm.

Bên cạnh thao tác dữ liệu, buổi học cũng nhấn mạnh việc cấu hình kết nối chính xác, quản lý tài nguyên hợp lý và sử dụng tham số SQL để hạn chế nguy cơ bảo mật. Đây là những kiến thức nền tảng cần thiết để xây dựng các chức năng quản lý hàng hóa và hỗ trợ nghiệp vụ bán lẻ trong hệ thống Mini Supermarket.

12. Tác Giả
- Đề tài: Phát triển Ứng dụng Thu ngân và Quản lý Hàng hóa cho Mô hình Bán lẻ – Cửa hàng Tiện lợi Alpha Mini
- Họ tên sinh viên: Mai Chí Cường
- Mã sinh viên: 2124110113
- Lớp học phần: CCQ2411D
