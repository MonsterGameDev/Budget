using Budget.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Extensions
{
    public static class CityInfoContextExtension
    {
        public static void EnsureSeedDataForContext(this BudgetDbContext context)
        {
            if (context.Accounts.Any())
            {
                return;
            }

            var accounts = new List<Account>() {
                new Account()
                {
                    Name = "Gaver Jul",
                    Description = "Julegaver",
                    SubAccounts = new List<SubAccount>(){
                        new SubAccount(){
                            Name = "Mor",
                            Description = "",
                            PostingLines = new List<PostingLine>(){
                                new PostingLine(){
                                    Description = "Creme",
                                    Location = "Matas",
                                    Amount = 175,
                                    Created = DateTime.Now
                                },
                                new PostingLine(){
                                    Description = "Bog - Claire Breichter",
                                    Location = "Arnold Busk",
                                    Amount = 244.50m,
                                    Created = DateTime.Now
                                },
                            }
                        },
                        new SubAccount(){
                            Name = "Far",
                            Description = "",
                            PostingLines = new List<PostingLine>(){
                            new PostingLine(){
                                    Description = "Pibe",
                                    Location = "Østerbro Tobaksforretning",
                                    Amount = 110,
                                    Created = DateTime.Now
                                },
                                new PostingLine(){
                                    Description = "Tegnebog",
                                    Location = "Magasin",
                                    Amount = 488.25m,
                                    Created = DateTime.Now
                                }}
                        },
                        new SubAccount(){
                            Name = "Bror",
                            Description = "",
                            PostingLines = new List<PostingLine>(){
                                new PostingLine(){
                                    Description = "Svæveflyver",
                                    Location = "Lyngby Hobby",
                                    Amount = 799,
                                    Created = DateTime.Now
                                }
                            }
                        },
                    }
                },
                new Account()
                {
                    Name = "Gaver Fødselsdage",
                    Description = "Fødselsdagsgaver",
                    SubAccounts = new List<SubAccount>(){}
                },
                new Account()
                {
                    Name = "Ferier",
                    Description = "Diverse faste ferier og udflugter",
                    SubAccounts = new List<SubAccount>(){}
                },
                new Account()
                {
                    Name = "Tøj",
                    Description = "Tøj budget",
                    SubAccounts = new List<SubAccount>(){}
                },
                new Account()
                {
                    Name = "Husholdning",
                    Description = "Dagligt forbrug: mad, cigaretter, personlig pleje mm",
                    SubAccounts = new List<SubAccount>(){}
                },
                new Account()
                {
                    Name = "Festivitas",
                    Description = "Diverse festivitas afholdt for vores penge",
                    SubAccounts = new List<SubAccount>(){}
                },
            };

            context.Accounts.AddRange(accounts);
            context.SaveChanges();
        }
    }
}
