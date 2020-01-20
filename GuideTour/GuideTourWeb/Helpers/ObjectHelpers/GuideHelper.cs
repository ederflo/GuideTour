using GuideTourData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Helpers.ObjectHelpers
{
    public class GuideHelper
    {
        public static List<Guide> SliceNames(List<Guide> guides, int maxLength, bool withDot = true)
        {
            foreach (Guide g in guides)
            {
                g.Name = StringHelper.Slice(g.Name, maxLength, withDot);
            }

            return guides;
        }

        public static Guide Clone(Guide g)
        {
            return new Guide()
            {
                Email = g.Email,
                Id = g.Id,
                Name = g.Name,
                TeamId = g.TeamId,
                Type = g.Type
            };
        }
    }
}
