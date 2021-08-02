<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

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
        <div style="width: 100%; height: 70%; display: flex; flex-flow: row; align-items: center; justify-content: flex-start; text-align: center">
            <div style="width: 15%; float: left;">
                <a href="UserInfo.aspx" style="position: absolute; top: 11rem;">使用者資訊</a>
                <a href="AccountingList.aspx" style="position: absolute; top: 14rem;">流水帳管理</a>
                <a href="/SystemAdmin/UserList.aspx" style="position: absolute; top: 17rem;">會員管理 </a>
            </div>
            <div style="width: 85%; height: 100%; float: right;">
                <table>
                    <tr>
                        <td>
                            <div style="text-align: left;">
                                <h1 style="margin-left: 10rem;">流水帳管理</h1>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="float: left; margin-left: 10rem;">
                                <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Add " />
                                <asp:Label runat="server" ID="lblAmount" Text="共 100 元" Style="margin-left: 10rem" Visible="true">
                                </asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="float: left; margin-top: 1.5rem; margin-left: 10rem;">
                                <!-- Main -->
                                <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAccountingList_RowDataBound" CellPadding="10" ForeColor="#333333" GridLines="Both">
                                    <AlternatingRowStyle BackColor="White" />
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
                                                <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">Edit</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                                <!-- 2021/08/02 -->
                                <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="AccountingList.aspx" />
                                <!-- 2021/08/02 -->
                                <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                                    <p style="color: red; background-color: cornflowerblue;">
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
