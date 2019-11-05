using System;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;
namespace TrainsInfo.DataParser.USOGDP
{
    public class USOGDPDaraParser : IDataParser
    {
        private readonly string PlanReceptionTrains = "PS_TR";
        private readonly string PlanReceptionVagons = "PS_VG";

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var input = data as string;
            var result = new List<RowValue>();
            if (input != null)
            {
                var parserModel = (new JavaScriptSerializer()).Deserialize<IList<ModelUSOGD>>(input);
                if (parserModel != null)
                {
                    var Joints = infrastructures.Where(x => x.Type == TypeInfrastructure.joint).ToList();
                    foreach (var joint in Joints)
                    {
                        var findJoint = parserModel.Where(x => joint.Station == x.esrStyk).FirstOrDefault();
                        if(findJoint != null)
                        {
                            result.Add(new RowValue(joint.Station, PlanReceptionTrains, findJoint.PSP.ToString(), DateTime.Now));
                            result.Add(new RowValue(joint.Station, PlanReceptionVagons, findJoint.VSP.ToString(), DateTime.Now));
                        }
                    }
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, PlanReceptionTrains, "0", DateTime.Now));
                        result.Add(new RowValue(x.Station, PlanReceptionVagons, "0", DateTime.Now));
                    });
                }
            }
            //
            return result;
        }
    }

    public class ModelUSOGD
    {
        public int VSP { get; set; }
        public int PSP { get; set; }
        public int esrStyk { get; set; }
    }
}
