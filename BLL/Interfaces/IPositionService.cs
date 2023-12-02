using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPositionService
    {
        List<PositionDTO> GetPositions();

        PositionDTO GetPosition(int id);

        void CreatePosition(PositionDTO position);
        void UpdatePosition(PositionDTO position);
        void DeletePosition(int id);
    }
}
