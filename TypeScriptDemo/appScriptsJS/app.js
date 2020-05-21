var $;
var toastr;
var CRUDModule;
(function (CRUDModule) {
    class UserDTO {
    }
    CRUDModule.UserDTO = UserDTO;
    class CrudComponent {
        getUserList() {
            fetch(`/api/users`).then(res => res.json()).then(res => {
                var response = res;
                if (response.userList != null) {
                    var userData = response.userList;
                    var tbody = "";
                    for (var i = 0; i < userData.length; i++) {
                        tbody += `<tr>
                                       <td>${userData[i][`Name`]}</td>
                                       <td>${userData[i][`Email`]}</td>
                                       <td>${userData[i][`Phone`]}</td>
                                       <td><i class="fa fa-edit" onclick="getUserById('${userData[i][`Id`]}')"></i><i class="fa fa-times" onclick="deleteUserById('${userData[i][`Id`]}')"></i></td>
                                    <tr>`;
                    }
                    $('#tblUsers tbody').html(tbody);
                }
            });
        }
        getUserById(userId) {
            fetch(`/api/users/${userId}`).then(res => res.json()).then(res => {
                var response = res.user;
                if (response != null) {
                    $('#hdnId').val(response.Id);
                    $('#txtName').val(response.Name);
                    $('#txtEmail').val(response.Email);
                    $('#txtPhone').val(response.Phone);
                    $('#modalUser').modal('show');
                }
            });
        }
        modifyUser(userDTO) {
            var userForm = new FormData();
            userForm.append("userData", JSON.stringify(userDTO));
            fetch(`/api/users/modify`, {
                method: 'POST',
                body: userForm
            }).then(res => res.json()).then(res => {
                var response = res;
                if (response.isSuccess == true)
                    toastr.success(response.message);
                else
                    toastr.error(response.message);
                this.getUserList();
                $('#modalUser').modal('hide');
            });
        }
        deleteUserById(userId) {
            if (confirm("Are you sure you want to delete the record")) {
                fetch(`/api/users/delete?userId=${userId}`, {
                    method: 'GET'
                }).then(res => res.json()).then(res => {
                    var response = res;
                    if (response.isSuccess == true)
                        toastr.success(response.message);
                    else
                        toastr.error(response.message);
                    this.getUserList();
                });
            }
        }
    }
    CRUDModule.CrudComponent = CrudComponent;
})(CRUDModule || (CRUDModule = {}));
//# sourceMappingURL=app.js.map