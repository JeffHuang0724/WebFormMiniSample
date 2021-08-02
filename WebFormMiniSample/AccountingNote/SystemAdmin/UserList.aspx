<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>毛豆端火鍋組 - 流水帳管理系統</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; height: 30%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center">
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
                <a href="UserInfo.aspx" style="position:absolute; top: 11rem;">使用者資訊</a>
                <a href="AccountingList.aspx" style="position:absolute; top: 14rem;">流水帳管理</a>
                <a href="/SystemAdmin/UserList.aspx"style="position:absolute; top: 17rem;">會員管理 </a>
            </div>
            <div style="width: 85%; float: right;">
                <table>
                    <tr>
                        <td>
                            <div style="text-align: left;">
                                <h1 style="margin-left: 10rem;">會員管理</h1>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="float: left; margin-left: 10rem;">
                                <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Add " />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="float: left; margin-top: 1.5rem; margin-left: 10rem;">
                                <!-- Main -->
                                <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvUserList_RowDataBound" CellPadding="10" ForeColor="#333333" GridLines="Both">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField HeaderText="帳號" DataField="Account" />
                                        <asp:BoundField HeaderText="姓名" DataField="Name" />
                                        <asp:BoundField HeaderText="Email" DataField="Email" />
                                        <asp:TemplateField HeaderText="等級">
                                            <ItemTemplate>
                                                <asp:Literal runat="server" ID="ltUserLevel"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                                        <asp:TemplateField HeaderText="Act">
                                            <ItemTemplate>
                                                <a href="/SystemAdmin/UserDetail.aspx?UID=<%# Eval("ID") %>">Edit</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <SortedDescendingHeaderStyle BackColor="#820000" />
                                </asp:GridView>
                                <uc1:ucPager runat="server" ID="ucPager" PageSize="5" CurrentPage="1" TotalSize="5" Url="UserList.aspx" />
                                <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                                    <p style="color: red; background-color: cornflowerblue">
                                        No data in your Accounting Note.
                                    </p>
                                </asp:PlaceHolder>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
