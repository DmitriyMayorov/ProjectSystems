using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PositionService : IPositionService
    {
        private IDbRepos db;
        public PositionService(IDbRepos db)
        {
            this.db = db;
        }

        public bool SaveChanges()
        {
            return (db.Save() > 0) ? true : false;
        }

        public List<PositionDTO> GetPositions()
        {
            return db.Positions.GetList().Select(i => new PositionDTO(i)).ToList();
        }

        public PositionDTO GetPosition(int id)
        {
            return new PositionDTO(db.Positions.GetItem(id));
        }
    }
}
