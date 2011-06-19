﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class chessboard : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateChessboard();
        RestoreChessboard();
    }

    void GenerateChessboard()
    {
        const int SIZE = 32;
        Table chessboard = new Table();
        chessboard.Height = Configure.ROWS * SIZE;
        chessboard.Width = Configure.COLS * SIZE;
        for (int i = 0; i < Configure.ROWS; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 0; j < Configure.COLS; j++)
            {
                TableCell gridHolder = new TableCell();
                gridHolder.ID = String.Format("cell{0:X}{1:X}", i, j);
                gridHolder.CssClass = "cell";
                //gridHolder.Height = SIZE;
                //gridHolder.Width = SIZE;
                //gridHolder.Style["background-image"] = "url(aGrid2.png)";
                //gridHolder.Style["background-size"] = String.Format("{0}px {0}px", SIZE);

                ImageButton grid = new ImageButton();
                grid.ID = String.Format("grid{0:X}{1:X}", i, j);
                //grid.ImageUrl = "~/aNopeGrid2.png";
                grid.CssClass = Chessman.GRID_NOPE;
                grid.Click += new ImageClickEventHandler(Grid_Click);
                //grid.AlternateText = "+";
                //grid.Height = SIZE;
                //grid.Width = SIZE;

                gridHolder.Controls.Add(grid);
                tr.Controls.Add(gridHolder);
            }
            chessboard.Controls.Add(tr);
        }
        ChessboardHolder.Controls.Add(chessboard);
    }

    void RestoreChessboard()
    {
        // get board id or redirect
        if (CommonState.CurrBoardId == null)
        {
            // TODO
            Response.Redirect("~/boardlist.aspx");
            return;
        }

        // get Board object
        Board board = CommonState.GetBoardById(CommonState.CurrBoardId);
        if (board == null)
        {
            // TODO
            Response.Redirect("~/boardlist.aspx");
            return;
        }

        // restoring grid images
        for (int i = 0; i < Configure.ROWS; i++)
        {
            for (int j = 0; j < Configure.COLS; j++)
            {
                string controlId = String.Format("grid{0:X}{1:X}", i, j);
                Debug.WriteLine("restoring control: " + controlId);
                ImageButton grid = (ImageButton)FindControl(controlId);
                grid.CssClass = board.chessboard[i, j].GridType;
            }
        }

        // we are done
    }

    void Grid_Click(object sender, ImageClickEventArgs e)
    {
        // locate the position of clilcked grid
        ImageButton grid = ((ImageButton)sender);
        int i = Int32.Parse(grid.ID.Substring(3, 1)) - 1;
        int j = Int32.Parse(grid.ID.Substring(4, 1)) - 1;
        Debug.WriteLine("Clicked grid: " + i.ToString() + " " + j.ToString());

        // get the current Board
        Board board = CommonState.GetBoardById(CommonState.CurrBoardId);
        if (board == null)
        {
            Response.Redirect("~/boardlist.aspx");
            return;
        }

        // is user logged in?
        if (CommonState.UserGuid == null)
        {
            Response.Redirect("~/login.aspx");
            return;
        }

        // check if the user can move
        if (board.IsTurnOf(CommonState.UserGuid))
        {
            // make move
            Debug.WriteLine("making move");
            board.MakeMove(i, j);
            board.ToggleTurn();
        }
        else
        {
            // TODO
            Debug.WriteLine("you are not eligible to make move");
            return;
        }
    }
}