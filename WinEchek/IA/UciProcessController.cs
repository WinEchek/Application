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
            Console.WriteLine("uci");

            string output = "";
            while (output != "uciok")
            {
                output = _uciProcess.StandardOutput.ReadLine();
                Console.WriteLine(output);
            }
            _uciProcess.StandardInput.WriteLine("ucinewgame");
            //Console.WriteLine(Environment.ProcessorCount);
            _uciProcess.StandardInput.WriteLine("setoption name Threads value {0}", Environment.ProcessorCount);
            Console.WriteLine("ucinewgame");
        }

        public override void Play()
        {
            PlayAsync();
        }
        
        private async void PlayAsync()
        {
            Console.WriteLine(FenTranslator.FenNotation(_container));
            await _uciProcess.StandardInput.WriteLineAsync("position fen " + FenTranslator.FenNotation(_container));
            await _uciProcess.StandardInput.WriteLineAsync("go movetime 2000");
            

            string input = new string(' ', 1);

            while (input == null || !input.Contains("bestmove"))
            {
                input = await _uciProcess.StandardOutput.ReadLineAsync();
                if(input != null)
                    Console.WriteLine(input);
            }

            if (!input.Contains("(none)"))
            {
                Coordinate startCoordinate = new Coordinate(input[9] - 'a', 7 - (input[10] - '1'));
                Coordinate targCoordinate = new Coordinate(input[11] - 'a', 7 - (input[12] - '1'));

                Move(new Move(_container.Board.PieceAt(startCoordinate), _container.Board.SquareAt(targCoordinate)));
            }
            
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