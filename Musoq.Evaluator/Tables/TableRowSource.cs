using System.Collections.Generic;
using System.Linq;
using Musoq.Schema.DataSources;

namespace Musoq.Evaluator.Tables
{
    public class TableRowSource : RowSource
    {
        private readonly IDictionary<string, int> _columnToIndexMap;
        private readonly Table _table;

        public TableRowSource(Table rowSource)
        {
            _table = rowSource;
            _columnToIndexMap = new Dictionary<string, int>();

            foreach (var column in _table.Columns)
                _columnToIndexMap.Add(column.Name, column.ColumnOrder);
        }

        public TableRowSource(Table rowSource, IDictionary<string, int> columnToIndexMap)
        {
            _table = rowSource;
            _columnToIndexMap = columnToIndexMap;
        }

        public override IEnumerable<IObjectResolver> Rows =>
            _table.Select(row => new RowResolver(row, _columnToIndexMap));
    }
}