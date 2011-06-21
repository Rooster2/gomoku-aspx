<%@ Page Language="C#" AutoEventWireup="true" CodeFile="boardlist.aspx.cs" Inherits="entrance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" Interval="1000" runat="server"></asp:Timer>
        <div>
            <asp:Label ID="labelWelcome" runat="server" Text="[labelWelcome]"></asp:Label>
            |
            <asp:LinkButton ID="linkLogout" runat="server" Text="Logout/Change name" 
                onclick="linkLogout_Click"></asp:LinkButton>
            <br />
            <br />
            <asp:Label ID="labelNewBoard" runat="server">New room:</asp:Label>
            <asp:TextBox ID="textboxBoardNick" runat="server" Width="257px"></asp:TextBox>
            <asp:Button ID="buttonStartANewBoard" Text="Create new room" runat="server" onclick="buttonStartANewBoard_Click" />
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
