var vmDetailUser = new Vue({
    el: '#AddOrUpdateUser',
    data: {
        UserId: 0,
        UserName: "",
        Email: "",
        Password: "",
        Phone: "",
        Birthday: 0,
        Name: "",
        RoleId: 0,
        lstRole: null,
        DetailUser: {},
        TitlePage: "Thêm mới tài khoản",
    },
    methods: {
        getListRole: function () {
            var self = this;
            $.ajax({
                url: '/api/UserApi/GetListRole',
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res != null) {
                    self.lstRole = null;
                    self.lstRole = JSON.parse(res);
                }
            })
        },
        getDetailUser: function () {
            var self = this;
            if (!location.href.includes('=')) {
                return false;
            }
            self.UserId = location.href.split('=')[1];
            if (!self.UserId) {
                return false;
            }
            $.ajax({
                url: '/api/UserApi/GetUserById?Id=' + self.UserId,
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res != null) {
                    self.DetailUser = {};
                    self.DetailUser = JSON.parse(res);
                    self.UserId = self.DetailUser.UserId;
                    self.UserName = self.DetailUser.UserName;
                    self.Email = self.DetailUser.Email;
                    self.Password = self.DetailUser.Password;
                    self.Phone = self.DetailUser.Phone;
                    self.Birthday = self.DetailUser.Birthday;
                    self.Name = self.DetailUser.Name;
                    self.RoleId = self.DetailUser.RoleId;
                }
            })
        },
        Submit: function (type) {
            var self = this;
            var dataUser = {};
            dataUser.UserId = self.UserId;
            dataUser.UserName = self.UserName;
            dataUser.Email = self.Email;
            dataUser.Password = self.Password;
            dataUser.Phone = self.Phone;
            dataUser.Birthday = self.Birthday;
            dataUser.Name = self.Name;
            dataUser.RoleId = self.RoleId;

            if (type === 0) {
                $.ajax({
                    url: '/api/UserApi/PostUserByAdmin',
                    data: dataUser,
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                }).then(res => {
                    if (res === 1) {
                        alert('Thêm thành công');
                    }
                    else {
                        alert('Lỗi');
                    }
                });
            }
            else if (type === 1) {
                 $.ajax({
                     url: '/api/UserApi/PostUserByAdmin',
                    data: dataProduct,
                    type: 'PUT',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                 }).then(res => {
                    if (res === 2) {
                        alert('Sửa thành công');
                    }
                    else {
                        alert('Lỗi');
                    }
                });
            }
            else {
                self.UserId = location.href.split('=')[1];
                var conf = confirm('Bạn chắc chắn muốn xóa tài khoản này?');
                if (conf) {
                    $.ajax({
                        url: '/api/UserApi/RemoveUserByAdmin?UserId=' + self.UserId,
                        type: 'DELETE',
                        dataType: 'json',
                        contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                    }).then(res => {
                        if (res === 1) {
                            alert('Xóa thành công');
                            window.location = 'https://localhost:44305/User/ManageUser';
                        }
                        else if (res === 2) {
                            alert('Không thể xóa tài khoản có quyền ADMIN');
                        }
                        else if (res === 3) {
                            alert('Tài khoản không tồn tại');
                        }
                        else {
                            alert('Lỗi');
                        }
                    })
                }
            }
        },
        getTitlePage: function () {
            var self = this;
            if (location.href.includes('=')) {
                self.TitlePage = "Cập nhật thông tin sản phẩm";
            }
            else {
                self.TitlePage = "Thêm mới sản phẩm";
            }
        }
    }
});
$(document).ready(function () {
    vmDetailUser.getDetailUser();
    //    vmCreateProduct.getCollection();
})
