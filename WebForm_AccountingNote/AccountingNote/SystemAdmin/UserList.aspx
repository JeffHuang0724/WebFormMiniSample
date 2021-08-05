<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1 style="margin-left: 10rem;">會員管理</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader2" runat="server">
    <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Add" Font-Size="Medium" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <div style="margin-top: 0.5rem;">
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
                <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
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
        <div style="margin-top: 1rem;">
            <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="/SystemAdmin/UserList.aspx" />
            <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                <p style="color: red; background-color: cornflowerblue">
                    No data in your Accounting Note.
                </p>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
