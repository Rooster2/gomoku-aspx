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

        <asp:Label ID="labelWelcome" runat="server" Text="[labelWelcome]"></asp:Label>
        <br />

        <asp:LinkButton ID="linkBackToBoardList" Text="<< back" runat="server" 
            onclick="linkBackToBoardList_Click" />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                White:
                <asp:PlaceHolder ID="WhiteSideHolder" runat="server"></asp:PlaceHolder>
<br />
                Black:&nbsp;<asp:PlaceHolder ID="BlackSideHolder" runat="server"></asp:PlaceHolder>
                <br />
                <asp:PlaceHolder ID="WinnerIs" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
			<ContentTemplate>
                <asp:PlaceHolder ID="ChessboardHolder" runat="server" />
            </ContentTemplate>
			<Triggers>
				<asp:AsyncPostBackTrigger ControlID="TimerBoard" EventName="Tick" />
			</Triggers>
		</asp:UpdatePanel>

        <br />

        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
			<ContentTemplate>
            <!--
                <asp:Literal Text="Players" runat="server" />
                <asp:BulletedList ID="ListPlayers" runat="server" />
                <br />
                -->
                <asp:Literal Text="Persons" runat="server" />
                <asp:BulletedList ID="ListViewers" runat="server" />
            </ContentTemplate>
			<Triggers>
				<asp:AsyncPostBackTrigger ControlID="TimerPersons" EventName="Tick" />
			</Triggers>
		</asp:UpdatePanel>
		
    </form>
</body>
</html>
