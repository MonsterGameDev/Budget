using Budget.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Mock
{
    public class AccountsDataStore
    {
        public static AccountsDataStore Current = new AccountsDataStore();
        public List<AccountDto> Accounts { get; set; }

        public AccountsDataStore()
        {
            Accounts = new List<AccountDto>() {
                new AccountDto()
                {
                    Id = 1,
                    Name = "Gaver Jul",
                    Description = "Julegaver",
                    PostingLines = new List<PostingLineDto>(){ },
                    SubAccounts = new List<SubAccountDto>(){
                        new SubAccountDto(){
                            Id = 1,
                            Name = "Mor",
                            Description = "",   
                            PostingLines = new List<PostingLineDto>(){
                                new PostingLineDto(){
                                    Id = 1,
                                    Description = "Creme",
                                    Location = "Matas",
                                    Amount = 175,
                                    Created = new DateTime()
                                },
                                new PostingLineDto(){
                                    Id = 2,
                                    Description = "Bog - Claire Breichter",
                                    Location = "Arnold Busk",
                                    Amount = 244.50,
                                    Created = new DateTime()
                                },
                            }
                        },
                        new SubAccountDto(){
                            Id = 2,
                            Name = "Far",
                            Description = "",
                            PostingLines = new List<PostingLineDto>(){
                            new PostingLineDto(){
                                    Id = 3,
                                    Description = "Pibe",
                                    Location = "Østerbro Tobaksforretning",
                                    Amount = 110,
                                    Created = new DateTime()
                                },
                                new PostingLineDto(){
                                    Id = 4,
                                    Description = "Tegnebog",
                                    Location = "Magasin",
                                    Amount = 488.25,
                                    Created = new DateTime()
                                }}
                        },
                        new SubAccountDto(){
                            Id = 3,
                            Name = "Bror",
                            Description = "",
                            PostingLines = new List<PostingLineDto>(){
                                new PostingLineDto(){
                                    Id = 5,
                                    Description = "Svæveflyver",
                                    Location = "Lyngby Hobby",
                                    Amount = 799,
                                    Created = new DateTime()
                                }
                            }
                        },
                    }
                },
                new AccountDto()
                {
                    Id = 2,
                    Name = "Gaver Fødselsdage",
                    Description = "Fødselsdagsgaver",
                    PostingLines = new List<PostingLineDto>(){ },
                    SubAccounts = new List<SubAccountDto>(){}
                },
                new AccountDto()
                {
                    Id = 3,
                    Name = "Ferier",
                    Description = "Diverse faste ferier og udflugter",
                    PostingLines = new List<PostingLineDto>(){ },
                    SubAccounts = new List<SubAccountDto>(){}
                },
                new AccountDto()
                {
                    Id = 4,
                    Name = "Tøj",
                    Description = "Tøj budget",
                    PostingLines = new List<PostingLineDto>(){ },
                    SubAccounts = new List<SubAccountDto>(){}
                },
                new AccountDto()
                {
                    Id = 5,
                    Name = "Husholdning",
                    Description = "Dagligt forbrug: mad, cigaretter, personlig pleje mm",
                    PostingLines = new List<PostingLineDto>(){ },
                    SubAccounts = new List<SubAccountDto>(){}
                },
                new AccountDto()
                {
                    Id = 6,
                    Name = "Festivitas",
                    Description = "Diverse festivitas afholdt for vores penge",
                    PostingLines = new List<PostingLineDto>(){ },
                    SubAccounts = new List<SubAccountDto>(){}
                },
            };
        }
    }
}
