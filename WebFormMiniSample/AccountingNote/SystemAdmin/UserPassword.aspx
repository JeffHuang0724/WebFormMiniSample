<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>毛豆端火鍋組 - 流水帳管理系統</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; height: 30%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center;">
            <table>
                <tr>
                    <td>
                        <h1>流水帳管理系統   -  後台</h1>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%; height: 70%; display: flex; flex-flow: row; align-items: center; justify-content: flex-start; text-align: center;">
            <div style="width: 15%; float: left;">
                <a href="UserInfo.aspx" style="position: absolute; top: 11rem;">使用者資訊</a>
                <a href="AccountingList.aspx" style="position: absolute; top: 14rem;">流水帳管理</a>
                <a href="/SystemAdmin/UserList.aspx" style="position: absolute; top: 17rem;">會員管理 </a>
            </div>
            <div style="width: 85%; float: right;">
                <div style="text-align: left; margin-left: 10rem;">
                    <h1>會員管理</h1>
                </div>
                <div style="float: left; margin-top: 1.5rem; margin-left: 10rem;">
                    <table>
                        <tr>
                            <td>
                                <div style="float: left;">帳號: </div>
                            </td>
                            <td>
                                <div style="float: left; margin-left: 1rem;">
                                    <asp:Label runat="server" ID="lblUserAccount"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="float: left; margin-top: 1rem;">原密碼:</div>
                            </td>
                            <td>
                                <div style="float: left; margin-left: 1rem; margin-top: 1rem;">
                                    <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="float: left; margin-top: 1rem;">確認密碼:</div>
                            </td>
                            <td>
                                <div style="float: left; margin-left: 1rem; margin-top: 1rem;">
                                    <asp:TextBox ID="txtOldCommitPwd" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="float: left; margin-top: 1rem;">新密碼:</div>
                            </td>
                            <td>
                                <div style="float: left; margin-left: 1rem; margin-top: 1rem;">
                                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="float: left;">
                                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="變更密碼" Style="margin-top: 1rem;" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Literal runat="server" ID="ltMsg"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
