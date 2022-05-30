var vmAdminLogin = new Vue({
    el: '#AdminLogin',
    data: {
        UserName: '',
        Password: '',
        LoginMessage:'',
    },
    methods: {
        Login: function () {
            var self = this;
            if (!self.UserName || !self.Password) {
                self.LoginMessage = 'Vui lòng nhập đầy đủ tài khoản và mật khẩu';
                return false;
            }
            var dataLogin = {};
            dataLogin.UserName = self.UserName;
            dataLogin.Password = self.Password;
            $.ajax({
                url: '/UserApi/AdminLogin',
                data: dataLogin,
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"

            }).then(res => {
                if (res !== null) {
                    //vmAdminLogin.IdUser = JSON.parse(res).Id;
                    //vmAdminLogin.UserFullName = JSON.parse(res).Name;
                    //vmAdminLogin.UserRole = JSON.parse(res).Role;
                    alert('Đăng nhập thành công');
                    window.location = '/Product/ManageProduct';
                }
                else {
                    self.LoginMessage = "Tài khoản hoặc mật khẩu không đúng!";
                }
            });
        }
    }
})