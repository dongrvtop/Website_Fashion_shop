var vmModalLogin = new Vue({
    el: '#ModalLogin',
    data: {
        isRegis: false,
        UserNameLg: "",
        PasswordsLg: "",
        UserNameRg: "",
        UserFullNameRg:"",
        PhoneNumber:"",
        PasswordsRg: "",
        ErrorMessageLogin:"",
        ErrorMessageRegis:"",
    },
    methods: {
        Login: function () {
            var self = this;
            if (!self.UserNameLg || !self.PasswordsLg) {
                self.ErrorMessageLogin = "Vui lòng nhập đủ tài khoản và mật khẩu!";
                return false;
            }
            var dataLogin = {};
            dataLogin.UserName = self.UserNameLg;
            dataLogin.Password = self.PasswordsLg;
            $.ajax({
                url: '/UserApi/Login',
                data: dataLogin,
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"

            }).then(res => {
                if (res !== null) {
                    //vmLayout.IdUser = JSON.parse(res).Id;
                    //vmLayout.UserFullName = JSON.parse(res).Name;
                    //vmLayout.UserRole = JSON.parse(res).Role;
                    alert('Đăng nhập thành công');
                   
                    $('#ModalLogin').modal('hide');
                    window.location.reload();
                    
                }
                else {
                    self.ErrorMessageLogin = "Tài khoản hoặc mật khẩu không đúng!";
                }
            });
        },
        Regis: function () {
            var self = this;
            if (!self.UserNameRg || !self.PhoneNumber || !self.PasswordsRg) {
                self.ErrorMessageLogin = "Vui lòng nhập đủ tài khoản và mật khẩu!";
                return false;
            }
            var dataRegis = {};
            dataRegis.UserName = self.UserNameRg;
            dataRegis.Name = self.UserFullNameRg;
            dataRegis.Phone = self.PhoneNumber;
            dataRegis.Password = self.PasswordsRg;
            $.ajax({
                url: '/api/UserApi/Register',
                data: dataRegis,
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"

            }).then(res => {
                if (res === 1) {
                    self.UserNameRg = "";
                    self.UserFullNameRg = "";
                    self.PhoneNumber = "";
                    self.PasswordsRg = "";
                    alert('Đăng ký thành công');
                    $('#ModalLogin').modal('hide');
                }
                else if (res === 2) {
                    self.ErrorMessageLogin = "Tài khoản đã tồn tại, vui lòng nhập lại tên tài khoản!";
                }
                else if (res === 3) {
                    self.ErrorMessageLogin = "Email đã dược sử dụng, vui lòng nhập lại tên tài khoản!";
                }
                else {
                    self.ErrorMessageLogin = "Đăng ký thất bại";
                }
            });
        }
    },
    watch: {
        UserNameLg: function () {
            var self = this;
            if (self.UserNameLg != null && self.UserNameLg != '') {
                self.ErrorMessageLogin = "";
            }
        },
        PasswordsLg: function () {
            var self = this;
            if (self.PasswordsLg != null && self.PasswordsLg != '') {
                self.ErrorMessageLogin = "";
            }
        },
        UserNameRg: function () {
            var self = this;
            if (self.UserNameRg != null && self.UserNameRg != '') {
                self.ErrorMessageRegis = "";
            }
        },
        PhoneNumber: function () {
            var self = this;
            if (self.PhoneNumber != null && self.PhoneNumber != '') {
                self.ErrorMessageRegis = "";
            }
        },
        PasswordsRg: function () {
            var self = this;
            if (self.PasswordsRg != null && self.PasswordsRg != '') {
                self.ErrorMessageRegis = "";
            }
        }
    }
})