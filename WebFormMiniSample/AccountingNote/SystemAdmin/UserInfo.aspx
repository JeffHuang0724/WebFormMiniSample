<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>毛豆端火鍋組 - 流水帳管理系統</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; height: 100%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center">
            <table>
                <tr>
                    <td>
                        <h1>流水帳管理系統   -  後台</h1>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%; height: 100%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center">
            <table width="100%">
                <tr>
                    <td width="15%">
                        <div>
                            <a href="UserInfo.aspx">使用者資訊</a>
                            <br />
                            <br />
                            <a href="AccountingList.aspx">流水帳管理</a>
                            <br />
                            <br />
                            <a href="/SystemAdmin/UserList.aspx">會員管理 </a>
                        </div>
                    </td>
                    <td width="85%">
                        <table>
                            <tr>
                                <td>
                                    <div style="text-align: left">
                                        <h1 style="margin-left: 15rem">個人資訊</h1>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="float: left; margin-left: 15rem;">
                                        <table style="line-height: 2rem;" border="1">
                                            <tr>
                                                <th>Account</th>
                                                <td>
                                                    <asp:Literal runat="server" ID="ltAccount"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Name</th>
                                                <td>
                                                    <asp:Literal runat="server" ID="ltName"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>E-mail</th>
                                                <td>
                                                    <asp:Literal runat="server" ID="ltEmail"></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="margin-top: 1rem">
                                            <tr>
                                                <td>
                                                    <asp:Button runat="server" ID="btnLogout" OnClick="btnLogout_Click" Text="Logout" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
