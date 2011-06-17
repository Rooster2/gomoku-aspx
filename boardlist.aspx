<%@ Page Language="C#" AutoEventWireup="true" CodeFile="boardlist.aspx.cs" Inherits="entrance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" Interval="1000" runat="server"></asp:Timer>
        <div>
            <asp:Label ID="labelWelcome" runat="server" Text="[labelWelcome]"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server">New Room:</asp:Label>
            <asp:TextBox ID="textboxRoomName" runat="server"></asp:TextBox>
            <asp:Button ID="buttonBuildNewRoom" Text="Create new room" runat="server" onclick="buttonBuildNewRoom_Click" />
        </div>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
