<%@ Page Language="C#" AutoEventWireup="true" CodeFile="board.aspx.cs" Inherits="board" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title>Chessboard</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />
		<asp:Timer ID="TimerBoard" runat="server" Interval="1500"></asp:Timer>
        <asp:Timer ID="TimerPersons" runat="server" Interval="3000"></asp:Timer>

        <asp:LinkButton ID="linkBackToBoardList" Text="<< back" runat="server" 
            onclick="linkBackToBoardList_Click" />&nbsp;|
		<asp:Button ID="buttonRestart" runat="server" Text="Restart" />
        <br />
        <br />
        <table border="0">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
			            <ContentTemplate>
                            <asp:PlaceHolder ID="ChessboardHolder" runat="server" />
                        </ContentTemplate>
			            <Triggers>
				            <asp:AsyncPostBackTrigger ControlID="TimerBoard" EventName="Tick" />
			            </Triggers>
		            </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
			            <ContentTemplate>
                            <asp:Literal Text="Players" runat="server" />
                            <asp:BulletedList ID="ListPlayers" runat="server" />
                            <br />
                            <asp:Literal Text="Viewers" runat="server" />
                            <asp:BulletedList ID="ListViewers" runat="server" />
                        </ContentTemplate>
			            <Triggers>
				            <asp:AsyncPostBackTrigger ControlID="TimerPersons" EventName="Tick" />
			            </Triggers>
		            </asp:UpdatePanel>
                </td>
            </tr>
        </table>
		
    </form>
</body>
</html>
