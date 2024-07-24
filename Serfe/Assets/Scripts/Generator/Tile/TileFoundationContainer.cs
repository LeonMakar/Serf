using Serfe.EventBusSystem;
using System.Collections.Generic;

namespace Serfe.TileContainer
{
    public class TileFoundationContainer
    {
        private readonly Dictionary<int, FoundationTile> _tileFoundationDictionary = new Dictionary<int, FoundationTile>();
        private EventBus _eventBus;
        private int _elementKeyToDiactivate;

        public TileFoundationContainer(EventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Subscrube<OnCheckPlayerPosition>(DiactivateFoundation);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OnCheckPlayerPosition>(DiactivateFoundation);
        }


        public void AddNewFoundationTile(int foundationPosition, FoundationTile foundation)
        {
            if (!_tileFoundationDictionary.ContainsKey(foundationPosition))
                _tileFoundationDictionary.Add(foundationPosition, foundation);
            else
                _tileFoundationDictionary[foundationPosition] = foundation;
        }


        public void DiactivateFoundation(OnCheckPlayerPosition onCheckPlayerSignal)
        {
            if ((_elementKeyToDiactivate + 25) < onCheckPlayerSignal.PositionOfZCoordinate)
            {
                UnityEngine.Debug.Log("firstKey = " + _elementKeyToDiactivate);
                UnityEngine.Debug.Log("playerpost = " + onCheckPlayerSignal.PositionOfZCoordinate);
                FoundationTile tileToDiactivate = _tileFoundationDictionary[_elementKeyToDiactivate];
                _tileFoundationDictionary.Remove(_elementKeyToDiactivate);
                DiactivationProccesFoundationTile(tileToDiactivate);
                _elementKeyToDiactivate += 20;
            }
        }

        private static void DiactivationProccesFoundationTile(FoundationTile tileToDiactivate)
        {
            tileToDiactivate.gameObject.SetActive(false);
            foreach (var item in tileToDiactivate.TilePattern.BonusTiles)
            {
                item.gameObject.SetActive(false);
                foreach (var bonus in item.BonusesList)
                    bonus.gameObject.SetActive(true);
            }
        }

        public void ResetAllFoundation()
        {
            foreach (var item in _tileFoundationDictionary)
                DiactivationProccesFoundationTile(item.Value);
            _tileFoundationDictionary.Clear();
        }
    }
}