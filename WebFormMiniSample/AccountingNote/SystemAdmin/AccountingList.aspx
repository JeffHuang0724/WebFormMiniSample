<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

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
                                        <h1 style="margin-left: 15rem">流水帳管理</h1>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="float: left; margin-left: 15rem;">
                                        <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Add " />
                                        <asp:Label runat="server" ID="lblAmount" Text="共 100 元" Style="margin-left: 10rem" Visible="true">
                                        </asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="float: left; margin-top: 1.5rem; margin-left: 15rem;">
                                        <!-- Main -->
                                        <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAccountingList_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                                                <asp:TemplateField HeaderText="IN / OUT">
                                                    <ItemTemplate>
                                                        <!--  <%# ((int)Eval("ActType") == 0) ? "支出" : "收入" %> -->
                                                        <asp:Literal runat="server" ID="ltActType"></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="金額">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmount"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="標題" DataField="caption" />
                                                <asp:TemplateField HeaderText="Act">
                                                    <ItemTemplate>
                                                        <a href="/SystemAdmin/AccountingDetail.aspx?list_id=<%# Eval("ID") %>">Edit</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                                            <p style="color: red; background-color: cornflowerblue">
                                                No data in your Accounting Note.
                                            </p>
                                        </asp:PlaceHolder>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
