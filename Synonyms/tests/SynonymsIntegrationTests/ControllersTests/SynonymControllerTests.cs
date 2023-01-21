using Entities.Entities;
using Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace SynonymsIntegrationTests.ControllersTests
{
    [TestClass]
    public class SynonymControllerTests
    {
        private readonly SynonymsSetUpTestEnvironment _testEnvironment;

        public SynonymControllerTests()
        {
            _testEnvironment = new SynonymsSetUpTestEnvironment();
        }

        [TestMethod]
        public async Task AddSynonym_ThenItsAdded()
        {
            //Arrange
            string existWord = "A";

            AddSynonymDto addSynonymDto = new AddSynonymDto
            {
                Word = existWord,
                Synonym = "steen"
            };
            string addSynonymDtoJson = JsonConvert.SerializeObject(addSynonymDto, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });

            //Act
            var response = await _testEnvironment.TestClient.PostAsync($"api/Synonym/AddSynonym", new StringContent(addSynonymDtoJson, Encoding.UTF8, "application/json"));

            //Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(1, _testEnvironment.SynonymsRepository._synonyms.First().Synonyms.Count);
            Assert.AreEqual(addSynonymDto.Synonym, _testEnvironment.SynonymsRepository._synonyms.First().Synonyms.First().Text);
        }
        
        [TestMethod]
        public async Task AddNotValidSynonym_ThenBadRequest()
        {
            //Arrange
            string existWord = "Aa";

            AddSynonymDto addSynonymDto = new AddSynonymDto
            {
                Word = existWord,
                Synonym = "ste en"
            };
            string addSynonymDtoJson = JsonConvert.SerializeObject(addSynonymDto, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });

            //Act
            var response = await _testEnvironment.TestClient.PostAsync($"api/Synonym/AddSynonym", new StringContent(addSynonymDtoJson, Encoding.UTF8, "application/json"));

            //Assert
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest,response.StatusCode);
        }   
        
        [TestMethod]
        public async Task AddSameWordAsSynonym_ThenBadRequest()
        {
            //Arrange
            string existWord = "Aa";

            AddSynonymDto addSynonymDto = new AddSynonymDto
            {
                Word = existWord,
                Synonym = "aa"
            };
            string addSynonymDtoJson = JsonConvert.SerializeObject(addSynonymDto, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });

            //Act
            var response = await _testEnvironment.TestClient.PostAsync($"api/Synonym/AddSynonym", new StringContent(addSynonymDtoJson, Encoding.UTF8, "application/json"));

            //Assert
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest,response.StatusCode);
        }    
        
        [TestMethod]
        public async Task AddNotValidWord_ThenBadRequest()
        {
            //Arrange
            string existWord = "A a";

            AddSynonymDto addSynonymDto = new AddSynonymDto
            {
                Word = existWord,
                Synonym = "steen"
            };
            string addSynonymDtoJson = JsonConvert.SerializeObject(addSynonymDto, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });

            //Act
            var response = await _testEnvironment.TestClient.PostAsync($"api/Synonym/AddSynonym", new StringContent(addSynonymDtoJson, Encoding.UTF8, "application/json"));

            //Assert
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest,response.StatusCode);
        }
        
        [TestMethod]
        public async Task AddSynonymFromAnotherNode_ThenItsAddedAndNodeRemoved()
        {
            //Arrange
            string existWordA = "a";
            var A = new Word
            {
                Text = existWordA,
            };
            
            string existWordB = "b";
            var B = new Word
            {
                Text = existWordB,
            }; 
            
            string existWordC = "c";
            var C = new Word
            {
                Text = existWordC,
            }; 
            
            string existWordD = "d";
            var D = new Word
            {
                Text = existWordD,
            };
            
                        
            string existWordE = "e";
            var E = new Word
            {
                Text = existWordE,
            };

            A.Synonyms.Add(B);
            B.ParentSynonyms.Add(A);
            A.Synonyms.Add(C);
            C.ParentSynonyms.Add(A);
            
            D.Synonyms.Add(E);
            E.ParentSynonyms.Add(D);
            
            _testEnvironment.SynonymsRepository._synonyms.Add(A);
            _testEnvironment.SynonymsRepository._synonyms.Add(D);
            AddSynonymDto addSynonymDto = new AddSynonymDto
            {
                Word = existWordB,
                Synonym = existWordD
            };
            string addSynonymDtoJson = JsonConvert.SerializeObject(addSynonymDto, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });

            //Act
            var response = await _testEnvironment.TestClient.PostAsync($"api/Synonym/AddSynonym", new StringContent(addSynonymDtoJson, Encoding.UTF8, "application/json"));

            //Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(1, _testEnvironment.SynonymsRepository._synonyms.Count);
            Assert.AreEqual(addSynonymDto.Word, _testEnvironment.SynonymsRepository._synonyms.First().Synonyms.First().Text);
            Assert.IsTrue(_testEnvironment.SynonymsRepository._synonyms.First().Synonyms.First().Synonyms.Any(s=>s.Text==addSynonymDto.Synonym));
        }
        
        [TestMethod]
        public async Task AddAlreadySynonym_ThenNotAccepted()
        {
            //Arrange
            string existWord = "a";
            var item = new Word
            {
                Text = existWord,
            };
            item.Synonyms.Add(new Word
            {
                Text = "b",
            });
            _testEnvironment.SynonymsRepository._synonyms.Add(item);
            AddSynonymDto addSynonymDto = new AddSynonymDto
            {
                Word = existWord,
                Synonym = "b"
            };
            string addSynonymDtoJson = JsonConvert.SerializeObject(addSynonymDto, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });

            //Act
            var response = await _testEnvironment.TestClient.PostAsync($"api/Synonym/AddSynonym", new StringContent(addSynonymDtoJson, Encoding.UTF8, "application/json"));

            //Assert
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotAcceptable,response.StatusCode);
        }

        [TestMethod]
        public async Task RemoveAll_ThenAllRemoved()
        {
            //Arrange
            string existWordA = "a";
            var A = new Word
            {
                Text = existWordA,
            };
            
            string existWordB = "b";
            var B = new Word
            {
                Text = existWordB,
            }; 
            
            
            A.Synonyms.Add(B);
            B.ParentSynonyms.Add(A);

            _testEnvironment.SynonymsRepository._synonyms.Add(A);
            
            //Act
            var response = await _testEnvironment.TestClient.DeleteAsync($"api/Synonym/RemoveAll");
            
            //Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(0, _testEnvironment.SynonymsRepository._synonyms.Count);
        }

        [TestMethod]
        public async Task GetWordSynonyms_ThenReturnAll()
        {
            //Arrange
            string existWordA = "a";
            var A = new Word
            {
                Text = existWordA,
            };
            
            string existWordB = "b";
            var B = new Word
            {
                Text = existWordB,
            }; 
            
            string existWordC = "c";
            var C = new Word
            {
                Text = existWordC,
            }; 
            
            string existWordD = "d";
            var D = new Word
            {
                Text = existWordD,
            };
            
                        
            string existWordE = "e";
            var E = new Word
            {
                Text = existWordE,
            };  
            
            string existWordF = "f";
            var F = new Word
            {
                Text = existWordF,
            };

            A.Synonyms.Add(B);
            B.ParentSynonyms.Add(A);
            
            A.Synonyms.Add(C);
            C.ParentSynonyms.Add(A);
            
            B.Synonyms.Add(D);
            D.ParentSynonyms.Add(B);
            
            D.Synonyms.Add(E);
            E.ParentSynonyms.Add(D);
            
            D.Synonyms.Add(A);
            A.ParentSynonyms.Add(D);
            
            D.Synonyms.Add(F);
            F.ParentSynonyms.Add(D);
            
            _testEnvironment.SynonymsRepository._synonyms.Add(A);
            
            //Act
            var response = await _testEnvironment.TestClient.GetAsync($"api/Synonym/GetSynonyms?word={D.Text}");

            var synonyms =
                JsonConvert.DeserializeObject<List<SynonymsDto>>(await response.Content.ReadAsStringAsync());
            
            //Assert
            Assert.AreEqual(5, synonyms.Count);
            Assert.AreEqual(E.Text,synonyms[0].Text);
            Assert.AreEqual(A.Text,synonyms[1].Text);
            Assert.AreEqual(F.Text,synonyms[2].Text);
            Assert.AreEqual(B.Text,synonyms[3].Text);
            Assert.AreEqual(C.Text,synonyms[4].Text);
        }
        [TestMethod]
        public async Task GetWordSynonyms_ThenReturnAll2()
        {
            //Arrange
            string existWordA = "a";
            var A = new Word
            {
                Text = existWordA,
            };
            
            string existWordB = "b";
            var B = new Word
            {
                Text = existWordB,
            }; 
            
            string existWordC = "c";
            var C = new Word
            {
                Text = existWordC,
            }; 
            
            string existWordD = "d";
            var D = new Word
            {
                Text = existWordD,
            };
            
                        
            string existWordE = "e";
            var E = new Word
            {
                Text = existWordE,
            };  
            
            string existWordF = "f";
            var F = new Word
            {
                Text = existWordF,
            };     
            
            string existWordZ = "z";
            var Z = new Word
            {
                Text = existWordZ,
            };  
            
            string existWordW = "w";
            var W = new Word
            {
                Text = existWordW,
            };    
            
            string existWordX = "x";
            var X = new Word
            {
                Text = existWordX,
            };

            A.Synonyms.Add(B);
            B.ParentSynonyms.Add(A);

            D.Synonyms.Add(A);
            A.ParentSynonyms.Add(D);
            D.Synonyms.Add(B);
            B.ParentSynonyms.Add(D);
            D.Synonyms.Add(E);
            E.ParentSynonyms.Add(D);
            E.Synonyms.Add(F);
            F.ParentSynonyms.Add(E);

            B.Synonyms.Add(C);
            C.ParentSynonyms.Add(B);

            Z.Synonyms.Add(X);
            X.ParentSynonyms.Add(Z);
            Z.Synonyms.Add(W);
            W.ParentSynonyms.Add(Z);

            B.Synonyms.Add(Z);
            Z.ParentSynonyms.Add(B);
            B.Synonyms.Add(W);
            W.ParentSynonyms.Add(B);
            B.Synonyms.Add(X);
            X.ParentSynonyms.Add(B);
            
            _testEnvironment.SynonymsRepository._synonyms.Add(A);
            
            //Act
            var response = await _testEnvironment.TestClient.GetAsync($"api/Synonym/GetSynonyms?word={Z.Text}");

            var synonyms =
                JsonConvert.DeserializeObject<List<SynonymsDto>>(await response.Content.ReadAsStringAsync());
            
            //Assert
            Assert.AreEqual(8, synonyms.Count);
            Assert.AreEqual(X.Text,synonyms[0].Text);
            Assert.AreEqual(W.Text,synonyms[1].Text);
            Assert.AreEqual(B.Text,synonyms[2].Text);
            Assert.AreEqual(C.Text,synonyms[3].Text);
            Assert.AreEqual(A.Text,synonyms[4].Text);
            Assert.AreEqual(D.Text,synonyms[5].Text);
            Assert.AreEqual(E.Text,synonyms[6].Text);
            Assert.AreEqual(F.Text,synonyms[7].Text);
        }
        
        [TestMethod]
        public async Task GetForNewWord_ThenReturnEmpty()
        {
            //Arrange
           
            //Act
            var response = await _testEnvironment.TestClient.GetAsync($"api/Synonym/GetSynonyms?word=A");

            var synonyms =
                JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
            
            //Assert
            Assert.AreEqual(0, synonyms.Count);

        }
    }
}
