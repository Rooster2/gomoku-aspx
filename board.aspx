<%@ Page Language="C#" AutoEventWireup="true" CodeFile="board.aspx.cs" Inherits="board" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>a demo</title>
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
			    <div>
		            <asp:ImageButton ID="img11" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" />
                    <asp:ImageButton ID="img12" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" />
                    <asp:ImageButton ID="img13" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" /><br />
                    <asp:ImageButton ID="img21" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" />
                    <asp:ImageButton ID="img22" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" />
                    <asp:ImageButton ID="img23" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" /><br />
                    <asp:ImageButton ID="img31" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" />
                    <asp:ImageButton ID="img32" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" />
                    <asp:ImageButton ID="img33" runat="server" Height="128px" Width="128px" OnClick="Grid_Click" /><br />
                </div>
			</ContentTemplate>
			<Triggers>
				<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
			</Triggers>
		</asp:UpdatePanel>
    </form>
</body>
</html>
