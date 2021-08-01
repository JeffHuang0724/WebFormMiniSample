<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AccountingNote.Login" %>

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
                        <h1 style="margin-right: 10rem">流水帳管理系統</h1>
                    </td>
                    <td>
                        <a href="Login.aspx">登入系統</a>
                    </td>
                </tr>
            </table>
        </div>
        <asp:PlaceHolder ID="plcLogin" runat="server" Visible="true">
            <div style="width: 100%; height: 100%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center; margin-top:1rem">
                <table style="line-height:2rem;">
                    <tr>
                        <td>
                            Account:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:PlaceHolder>
    </form>
</body>
</html>
