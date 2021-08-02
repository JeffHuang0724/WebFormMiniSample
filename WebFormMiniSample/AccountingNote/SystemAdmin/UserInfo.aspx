<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserInfo" %>

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
        <div style="width: 100%; height: 70%; display: flex; flex-flow:row; align-items: center; justify-content: flex-start; text-align: center;">
            <div style="width: 15%; float: left;">
                <a href="UserInfo.aspx" style="position:absolute; top: 11rem;">使用者資訊</a>
                <a href="AccountingList.aspx" style="position:absolute; top: 14rem;">流水帳管理</a>
                <a href="/SystemAdmin/UserList.aspx"style="position:absolute; top: 17rem;">會員管理 </a>
            </div>
            <div style="width: 85%; float: right;">
                <table >
                    <tr>
                        <td>
                            <div style="text-align: left;">
                                <h1 style="margin-left: 10rem;">個人資訊</h1>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="float: left; margin-left: 10rem;">
                                <table cellpadding="15" style="width: 100%; text-align:left; line-height: 2rem;" border="1">
                                    <tr>
                                        <th style="width: 30%;">Account</th>
                                        <td style="width: 70%;">
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
                </table>
            </div>
        </div>
    </form>
</body>
</html>
