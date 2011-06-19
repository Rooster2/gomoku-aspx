<%@ Page Language="C#" AutoEventWireup="true" CodeFile="board.aspx.cs" Inherits="board" %>

<%@ Register src="chessboard.ascx" tagname="chessboard" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Chessboard</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />
		<asp:Timer ID="Timer1" runat="server" Interval="1000"></asp:Timer>

        <asp:Label ID="labelDebugBoardId" runat="server" Text="[boardId]"></asp:Label>
		<br />
		<asp:Button ID="buttonRestart" runat="server" Text="Restart" />
		<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
			<ContentTemplate>
                <uc1:chessboard ID="chessboard1" runat="server" />
            </ContentTemplate>
			<Triggers>
				<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
			</Triggers>
		</asp:UpdatePanel>
    </form>
</body>
</html>
