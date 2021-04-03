
using RegionBot.Const;
using RegionBot.Context;
using RegionBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegionBot
{
    public class RegionManager
    {


        public void Add()
        {
            Region region = new Region();
            EntityContext context = new EntityContext();
            //1.Seviye : İller Ekleme
            RequestService service = new RequestService();
            List<Region> cities = service.GetRegions(RegionConst.GetBBKCityList, RegionConst.Kod, RegionConst.lstIl);
            Console.WriteLine("-----------------------------------------1.SEVİYE BAŞLADI----------------------------------------");
            foreach (Region city in cities)
            {

                Console.WriteLine(" ");
                Console.WriteLine("    " + city.Name + "  ( ID : " + city.ID + ")");


                region.Alias = "il";
                region.Name = city.Name;
                context.Regions.AddRange(region);
                context.SaveChanges();

                city.ID = 4;

                List<Region> districts = service.GetRegions(RegionConst.GetBBKCountyList, RegionConst.IlKod + "" + city.ID, RegionConst.lstIlce);
                Console.WriteLine("-----------------------------------------2.SEVİYE BAŞLADI----------------------------------------");
                foreach (Region district in districts)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("    " + district.Name + "  ( ID : " + district.ID + ")");

                    Region region2 = new Region();
                    region2.Alias = "ilçe";
                    region2.Name = district.Name;
                    region2.ParentRegionID = region.ID;
                    context.Regions.AddRange(region2);
                    context.SaveChanges();


                    List<Region> townShips = service.GetRegions(RegionConst.GetBBKBucakList, RegionConst.IlceKod + "" + district.ID, RegionConst.lstBucak);

              
                    Console.WriteLine("-----------------------------------------3.SEVİYE BAŞLADI----------------------------------------");
                    foreach (Region townShip in townShips)
                    {


                        Console.WriteLine(" ");
                        Console.WriteLine("BUCAK ");
                        Console.WriteLine("    " + townShip.Name + "  ( ID : " + townShip.ID + ")");

                        Region region3 = new Region();
                        region3.Alias = "bucak";
                        region3.Name = townShip.Name;
                        region3.ParentRegionID = region2.ID;
                        context.Regions.AddRange(region3);
                        context.SaveChanges();
                  

                        //4.Seviye : Bucağa Ait Olan  Köyleri Ekle
                        List<Region> villages = service.GetRegions(RegionConst.GetBBKKoyList, RegionConst.BucakKod + "" + townShip.ID, RegionConst.lstKoy);
                        Console.WriteLine("-----------------------------------------4.SEVİYE BAŞLADI----------------------------------------");
                        foreach (Region village in villages)
                        {

                            Console.WriteLine(" ");
                            Console.WriteLine("KÖY ");
                            Console.WriteLine("    " + village.Name + "  ( ID : " + village.ID + ")");
                            //DB ye kayıt
                            Region region4 = new Region();
                            region4.Alias = "koy";
                            region4.Name = village.Name;
                            region4.ParentRegionID = region3.ID;
                            context.Regions.AddRange(region4);
                            context.SaveChanges();

                            //5.Seviye : Köye  Ait Olan Mahalleri Ekle  
                            List<Region> neighborhoods = service.GetRegions(RegionConst.GetBBKMahalleList, RegionConst.KoyKod + "" + village.ID, RegionConst.lstMahalle);
                            Console.WriteLine("-----------------------------------------5.SEVİYE BAŞLADI----------------------------------------");
                            foreach (Region neighborhood in neighborhoods)
                            {
                                Console.WriteLine(" ");
                                Console.WriteLine("MAHALLE ");
                                Console.WriteLine("    " + neighborhood.Name + "  ( ID : " + neighborhood.ID + ")");
                                //DB ye kayıt
                                Region region5 = new Region();
                                region5.Alias = "mahalle";
                                region5.Name = neighborhood.Name;
                                region5.ParentRegionID = region4.ID;
                                context.Regions.AddRange(region5);
                                context.SaveChanges();

                                //6.Seviye : Mahalleye Ait Olan  Caddeleri Ekle 
                                List<Region> streets = service.GetRegions(RegionConst.GetBBKCaddeList, RegionConst.MahalleKod + "" + neighborhood.ID, RegionConst.lstCadde);
                                Console.WriteLine("-----------------------------------------6.SEVİYE BAŞLADI----------------------------------------");
                                foreach (Region street in streets)
                                {
                                    Console.WriteLine(" ");
                                    Console.WriteLine("CADDE ");
                                    Console.WriteLine("    " + street.Name + "  ( ID : " + street.ID + ")");
                                    //DB ye kayıt
                                    Region region6 = new Region();
                                    region6.Alias = "cadde - sokak";
                                    region6.Name = street.Name;
                                    region6.ParentRegionID = region5.ID;
                                    context.Regions.AddRange(region6);
                                    context.SaveChanges();

                                    //7.Seviye : Caddeye Ait Olan  Binalar  
                                    List<Region> apartments = service.GetRegions(RegionConst.GetBBKBinaList, RegionConst.CaddeKod + "" + neighborhood.ID, RegionConst.lstBina);
                                    Console.WriteLine("-----------------------------------------7.SEVİYE BAŞLADI----------------------------------------");
                                    foreach (Region apartment in apartments)
                                    {
                                        if (apartment.ID != 0 && apartment.Name != null)
                                        {
                                            Console.WriteLine(" ");
                                            Console.WriteLine("BİNA ");
                                            Console.WriteLine("    " + apartment.Name + "  ( ID : " + apartment.ID + ")");
                                            //DB ye kayıt
                                            Region region7 = new Region();
                                            region7.Alias = "bina";
                                            region7.Name = apartment.Name;
                                            region7.ParentRegionID = region6.ID;
                                            context.Regions.AddRange(region7);
                                            context.SaveChanges();
                                        }
                                    }
                                    Console.WriteLine("-----------------------------------------7.SEVİYE BİTTİ----------------------------------------");



                                }
                                Console.WriteLine("-----------------------------------------6.SEVİYE BİTTİ----------------------------------------");
                            }
                            Console.WriteLine("-----------------------------------------5.SEVİYE BİTTİ----------------------------------------");
                        }
                        Console.WriteLine("--------------------------------------------------4.SEVİYE BİTTİ ---------------------------------------------");
                    }
                    Console.WriteLine("--------------------------------------------------3.SEVİYE BİTTİ ---------------------------------------------");
                }
                Console.WriteLine("--------------------------------------------------2.SEVİYE BİTTİ ---------------------------------------------");

            }
            Console.WriteLine("--------------------------------------------------1.SEVİYE BİTTİ ---------------------------------------------");


        }
    }
}
