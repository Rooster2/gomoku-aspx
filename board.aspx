<%@ Page Language="C#" AutoEventWireup="true" CodeFile="board.aspx.cs" Inherits="board" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title>Chessboard</title>
    <style type="text/css">
        .style1
        {
            width: 64px;
            height: 338px;
        }
        .style2
        {
            height: 338px;
        }
        .style3
        {
            width: 64px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />
		<asp:Timer ID="TimerBoard" runat="server" Interval="800"></asp:Timer>
        <asp:Timer ID="TimerPersons" runat="server" Interval="3000"></asp:Timer>

        <asp:Label ID="labelWelcome" runat="server" Text="Welcome: "></asp:Label>
        <asp:Literal runat="server" > | </asp:Literal>
        <asp:LinkButton ID="linkBackToBoardList" Text="back to the Board List" runat="server" 
            onclick="linkBackToBoardList_Click" />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                White:&nbsp;<asp:PlaceHolder ID="WhiteSideHolder" runat="server"></asp:PlaceHolder>
                <br />
                Black:&nbsp;<asp:PlaceHolder ID="BlackSideHolder" runat="server"></asp:PlaceHolder>
                <br />
                <asp:PlaceHolder ID="WinnerIs" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <table>
            <tr>
                <td class="style3">
                </td>
                <td class="style2">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
			            <ContentTemplate>
                            <asp:PlaceHolder ID="ChessboardHolder" runat="server" />
                        </ContentTemplate>
			            <Triggers>
				            <asp:AsyncPostBackTrigger ControlID="TimerBoard" EventName="Tick" />
			            </Triggers>
		            </asp:UpdatePanel>
                </td>
                <td class="style1">
                </td>
                <td class="style2">
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
		        </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

    </form>
</body>
</html>
