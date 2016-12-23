using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WinEchek.Core;
using WinEchek.Model;
using WinEchek.Model.Piece;
using WinEchek.Model.Utility;

namespace WinEchek.IA
{
    public class UciProcessController : PlayerControler
    {
        private Process _uciProcess;
        private Container _container;

        public UciProcessController(Container container)
        {
            _container = container;
            _uciProcess = new Process
            {
                StartInfo =
                {
                    FileName = "stockfish_64.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                }
            };

            _uciProcess.Start();
            _uciProcess.StandardInput.WriteLine("uci");

            string output = "";
            while (output != "uciok")
            {
                output = _uciProcess.StandardOutput.ReadLine();
                Console.WriteLine(output);
            }
            _uciProcess.StandardInput.WriteLine("ucinewgame");
        }

        public override void Play()
        {
            PlayAsync();
        }

        private async void PlayAsync()
        {
            _uciProcess.StandardInput.WriteLine("ucinewgame");
            await _uciProcess.StandardInput.WriteLineAsync("position fen " + FenTranslator.FenNotation(_container));
            await _uciProcess.StandardInput.WriteLineAsync("go movetime 2000");

            string input = new string(' ', 1);
            while (!input.Contains("bestmove"))
            {
                input = await _uciProcess.StandardOutput.ReadLineAsync();
                
            }

            input = input.Remove(0, 9).Remove(4);

            Coordinate startCoordinate = new Coordinate(input[0] - 'a', 7 - (input[1] - '1'));
            Coordinate targCoordinate = new Coordinate(input[2] - 'a', 7 - (input[3] - '1'));

            Move(new Move(_container.Board.PieceAt(startCoordinate), _container.Board.SquareAt(targCoordinate)));
        }

        public override void Move(Move move)
        {
            Player.Move(move);
        }

        public override void InvalidMove(List<string> reasonsList)
        {
            //throw new System.NotImplementedException();
        }

        public override List<Square> PossibleMoves(Piece piece)
        {
            throw new System.NotImplementedException();
        }

        public override void Stop()
        {
            //throw new System.NotImplementedException();
        }
    }
}