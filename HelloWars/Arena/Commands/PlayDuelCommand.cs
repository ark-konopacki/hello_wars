﻿using Arena.Interfaces;
using Common.Interfaces;
using Common.Utilities;

namespace Arena.Commands
{
    public class PlayDuelCommand : CommandBase
    {
        protected readonly IElimination _elimination;
        protected readonly IGame _game;
        protected readonly ScoreList _scoreList;

        public PlayDuelCommand(IElimination elimination, IGame game, ScoreList scoreList)
        {
            _scoreList = scoreList;
            _elimination = elimination;
            _game = game;
        }

        public override void Execute(object parameter = null)
        {
            
            var nextCompetitors = _elimination.GetNextCompetitors();
            if (nextCompetitors != null)
            {
                _game.Reset();
                foreach (var nextCompetitor in nextCompetitors)
                {
                    _game.AddCompetitor(nextCompetitor);
                }

                _game.Start();

                while (_game.PerformNextRound())
                {
                }

                var duelResult = _game.GetResults();
                _elimination.SetLastDuelResult(duelResult);
                _scoreList.SaveScore(duelResult);
            }
        }
    }
}