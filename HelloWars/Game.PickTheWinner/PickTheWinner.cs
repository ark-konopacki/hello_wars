﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Common.Attributes;
using Common.Interfaces;
using Common.Models;
using Game.PickTheWinner.UserControls;

namespace Game.PickTheWinner
{
    [GameType("PickTheWinner")]
    public class PickTheWinner : IGame
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private List<ICompetitor> _competitors;
        private Dictionary<ICompetitor, double> _result;
        public long RoundNumber { get; set; }

        public List<ICompetitor> Competitors
        {
            get { return _competitors; }
        }

        public bool PerformNextRound()
        {
            var random = _rand.Next();
            _result = new Dictionary<ICompetitor, double>
            {
                {Competitors[0], random%2}, 
                {Competitors[1], (random + 1)%2}
            };

            RoundNumber++;
            return false;
        }

        public UserControl GetVisualisation()
        {
            return new PickTheWinnerControl();
        }

        public IDictionary<ICompetitor, double> GetResults()
        {
            return _result;
        }

        public void AddCompetitor(ICompetitor competitor)
        {
            _competitors.Add(new Competitor(competitor));
        }

        public void Start()
        {
        }

        public void Reset()
        {
            RoundNumber = 0;
            _competitors = new List<ICompetitor>();
        }
    }
}
