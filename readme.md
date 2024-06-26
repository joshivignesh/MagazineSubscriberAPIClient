For running the program:

![alt text](image.png)

Clone the repository in specific machine using URL --> https://github.com/joshivignesh/MagazineSubscriberAPIClient.git  or Open with Github Desktop or Download Zip file and unzip

If any editor available like VSCode or VS 2022 ( Any edition ) click on the file with extension .sln and run the application

If no editor available make sure .NET Framework is available, open the folder where code in placed. Copy the folder name in any CLI ( like command prompt or Power Shell ) and type command --> DotNet run

================================================
Relationship and Data flow order

1. Fetch Token => 2. Get Category List along with retrieved Token from Step 1 => 3. Fetch MagazineId, Name for specific Category => 4. Fetch Subscriber detail along with Magazine Id's that they are subscribed to => Post the data of each Subscriber Guid whoever are subscribed all the type of Magazine 

================================================
Request and Response to be used
 
 1. Request
 Get the token for further use   -- http://magazinestore.azurewebsites.net/api/token by calling this URL the token
  Ex: Request URL -- http://magazinestore.azurewebsites.net/api/token
  
  Response
  {
    "success": true,
    "token": "GnRh76FOkk2gunCMwM26kA"
}

 2. Request
 Get the type of Category from  -- http://magazinestore.azurewebsites.net/api/categories/{{token}} by passing the token
  Ex: Request URL -- http://magazinestore.azurewebsites.net/api/categories/T2DJyxFFke1G4H90DkAJQ

 Response( Complete Json ):
 {
    "data": [
        "Animals",
        "Political",
        "News"
    ],
    "success": true,
    "token": "T2DJyxFFke1G4H90DkAJQ"
}
 
 3. Request
 Get http://magazinestore.azurewebsites.net/api/magazines/{{token}}/{{category}} -- id (MagazineId) for each Category
 Ex: Request URL -- http://magazinestore.azurewebsites.net/api/magazines/T2DJyxFFke1G4H90DkAJQ/Political
 
 Response ( Partial Json ):
 {
  "data": [
        {
            "id": 2,
            "name": "National Review",
            "category": "Political"
        },
        {
            "id": 4,
            "name": "The Nation",
            "category": "Political"
        }],
    "success": true,
    "token": "T2DJyxFFke1G4H90DkAJQ"
	}
	
4. Request 
Get http://magazinestore.azurewebsites.net/api/subscribers/{{token}} -- 
 Ex: Request URL -- http://magazinestore.azurewebsites.net/api/subscribers/gPcYs5X14kqA9iahNE7aw
  
  Response ( Partial Json ):
 {
    "data": [
        {
            "id": "e9dc7bf4-6470-4393-9aeb-ef6d2b3ddaa2",
            "firstName": "Ethan",
            "lastName": "Wright",
            "magazineIds": [
                5,
                9,
                7,
                2,
                6
            ]
        },
        {
            "id": "658e315b-066f-4491-8415-8d5fa8d918b9",
            "firstName": "Anna",
            "lastName": "Choi",
            "magazineIds": [
                6,
                8,
                3,
                1,
                5
            ]
        },
    "success": true,
    "token": "T2DJyxFFke1G4H90DkAJQ"
	}
	
5. Request 
POST http://magazinestore.azurewebsites.net/api/answer/gPcYs5X14kqA9iahNE7aw

Response 

{
  "subscribers": [ "068cdc5a-a6e0-470a-823c-12c2713ab5cd",
            "2117c2e0-6bd2-47a5-8169-a5c5c0750144",
            "51f141ae-7a25-425c-bf4d-f37b7383c1a0",
            "5cb2e885-77db-409b-bbd4-ea82a5848485",
            "870822af-449e-426d-aef5-5923857d2048",
            "b84ff438-8c6e-4d9b-8799-7caa0933285a" ]
}