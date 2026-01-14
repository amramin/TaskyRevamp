using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskViews
{
    public class ColumnPreferenceService
    {

        // Event triggered whenever columns are updated
        public event Action? OnColumnsChanged;

        private List<TaskColumnSetting> _columns = new();

        public List<TaskColumnSetting> Columns
        {
            get => _columns;
            set
            {
                _columns = value;
                OnColumnsChanged?.Invoke(); // Notify all subscribers
            }
        }

        public void UpdateColumns(List<TaskColumnSetting> columns)
        {
            _columns = columns;
            OnColumnsChanged?.Invoke();
        }
    }

}

