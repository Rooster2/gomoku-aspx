<%@ Page Language="C#" AutoEventWireup="true" CodeFile="boardlist.aspx.cs" Inherits="entrance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title>Gomoku - Board List</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" Interval="1000" runat="server"></asp:Timer>

        <asp:Label ID="labelWelcome" runat="server" Text="Welcome: "></asp:Label>
        <asp:Literal runat="server"> | </asp:Literal>
        <asp:LinkButton ID="linkLogout" runat="server" Text="Logout/Change name" 
                onclick="linkLogout_Click"></asp:LinkButton>
        <br />
        <br />
        <asp:Label runat="server">Join a Board:</asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <hr />
        <br />
        <asp:Label ID="labelNewBoard" runat="server">Start a new Board:</asp:Label>
        <asp:TextBox ID="textboxBoardNick" runat="server" Width="256px"></asp:TextBox>
        <asp:Button ID="buttonStartANewBoard" Text="Go" runat="server" onclick="buttonStartANewBoard_Click" />
    </form>
</body>
</html>
