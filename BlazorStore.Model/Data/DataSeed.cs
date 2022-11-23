using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorStore.Model.Data
{
    public static class DataSeed
    {
        public static OrderLine[] GetOrderLines()
        {
            var products = GetProducts();
            return new OrderLine[]
            {
                new OrderLine()
                {
                    Id = 1, Image = products[0].Image, OrderId = 1, ProductId = products[0].Id,
                    ProductName = products[0].Name, Quantity = 1, UnitPrice = products[0].Price
                },
                new OrderLine()
                {
                    Id = 2, Image = products[1].Image, OrderId = 1, ProductId = products[1].Id,
                    ProductName = products[1].Name, Quantity = 1, UnitPrice = products[1].Price
                }
            };
        }
        public static Order[] GetOrders()
        {
            var jon = GetUsers()[1];
            var products = GetProducts();
            var order = new Order()
            {
                Id = 1,
                Name = jon.Name,
                UserId = jon.Id,
                Address = jon.Address,
                ZipCode = jon.ZipCode,
                Country = jon.Country,
                State = jon.State,
                ValidUntilMonth = 12,
                ValidUntilYear = DateTime.Now.Year + 2,
                Amount = products[0].Price + products[1].Price,
                CardNumber = "XXXXXXXXXXXXXXXXXX",
                City = jon.City,
                CvcCode = "321",
                Date = DateTime.Now,
                Email = jon.Email,
                NameOnCard = jon.Name,
                Status = OrderStatus.Submitted,
            };
            return new[] {order};
        }

        public static Category[] GetCategories()
        {
            var men = new Category { Id = 1, Name = "Men", Image = "images/categories/men.jpg" };
            var women = new Category { Id = 2, Name = "Women", Image= "images/categories/women.jpg" };
            var accessories = new Category { Id = 3, Name = "Accessories", Image = "images/categories/accessories.jpg" };
            var homeDecor = new Category { Id = 4, Name = "Home & Decor", Image = "images/categories/homedecor.jpg" };
            return new[] { women, men, accessories, homeDecor };
        }

        public static Product[] GetProducts()
        {
            var categories = GetCategories();
            var men = categories.First(c => c.Id == 1);
            var women = categories.First(c => c.Id == 2);
            var accessories = categories.First(c => c.Id == 3);
            var homeDecor = categories.First(c => c.Id == 4);

            return new[] {
                new Product(1, women, "Ludlow sheath dress", "Our feminine wool-blend frock transitions seamlessly from day to night. We suggest wearing with classic heels and a standout strand necklace.", 305, "products/1ludlowsheath.jpg", false, 20),
                new Product(2, women, "Racer back maxi dress", "This classic maxi dress drapes beautifully throughout body and sweeps in a light A-line to the floor. Keep a casual chic look by pairing with a jean jacket or go glam with a statement necklace.", 226, "products/2racerbackmaxi.jpg", false, 5),
                new Product(3, women, "Essex pencil skirt", "A classic pencil skirt that feels refreshingly modern. Crafted from our lighweight refined wool, the Jane skirt is an essential piece for on-the-go professionals.", 185, "products/3essexpencilskirt.jpg", false, 10),
                new Product(4, women, "Park avenue pleat front trousers", "Break away from the trend with these elegant pleat front pants. The high waistline and wide leg creates a feminine sihouette that's flattering on any figure. Pair with d'Orsay pumps and a waist belt.", 245, "products/4parkavenuepleat.jpg", false, 5),
                new Product(5, women, "Dumbo boyfriend jean", "Our straight leg jeans combine the comfort of a boyfriend-fit with the clean look of tailoring. Wear yours cuffed with a collar blouse and feminine flats to look fresh even after a long flight", 210, "products/5dumboboyfriend.jpg"),
                new Product(6, women, "Tribeca skinny jean", "The perfect jean for travel. Extra strech for a comfortable and flattering fit. Pair with a loose fit top and nude pumps for a modern silhouette.", 185, "products/6tribecaskinny.jpg"),
                new Product(7, women, "Delancy cardigan sweater", "Refresh your knitwear collection with our silk blend top. Layer over a bold hue for maximum contrast.", 275, "products/7delancy.jpg"),
                new Product(8, women, "Ludlow oxford top", "No matter where you are in the world, a crisp, clean oxford is always in style.", 185, "products/8ludlowoxford.jpg"),
                new Product(9, women, "Nolita cami", "Cut from tissue-weight silk crepe de chine, this airy style features a ruched neckline with tie and an unfinished hem for a contrastinly rugged feel. Compliment yours with skinny jeans.", 150, "products/9nolitacami.jpg", true, 8),
                new Product(10, women, "Lafayette convertible dress", "This all day dress has a flattering silhouette and a convertible neckline to suit your mood. Wear tied and tucked in a sailor knot, or reverse it for a high tied feminine bow.", 340, "products/10lafayette.jpg"),
                new Product(11, women, "Elizabeth knit top", "The demure Elizabeth Knit features a semi sheer open weave and a forgiving silhouette. A nude camisole underneath keeps a stylish but conservative look.", 210, "products/11elisabeth.jpg"),
                new Product(12, women, "Tori tank", "A simple ribbed cotton tank. Great for layering.", 60, "products/12toritank.jpg"),
                new Product(13, men, "Flat front trouser", "Thanks to its timeless versatility, these flat front trousers are perfectly suited to wear wherever and still stand apart.", 195, "products/13flatfront.jpg"),
                new Product(14, men, "The essential boot cut jean", "The new standard in deni, this jean is the rightful favorite among our designers. Made from lightly distressed denim to achieve that perfectly broken-in feel.", 140, "products/14theessential.jpg", false, 15),
                new Product(15, men, "Bowery chino pants", "The slim and trim Bowery is a wear-to-work pant you'll actually want to wear. A clean style in our crisp, compact cotton twill, it's perfectly polished (but also comfortable enough for hanging out after hours).", 140, "products/15bowerychino.jpg"),
                new Product(16, men, "Core striped sport shirt", "This grommet closure sports shirt is wrinkle free straight from the dryer.", 125, "products/16corestriped.jpg"),
                new Product(17, men, "Lexington cardigan sweater", "lean, raglan sleeve cardigan with cosmopolitan appeal.", 180, "products/17lexington.jpg"),
                new Product(18, men, "Merino v-neck pullover sweater", "A classy V-neck sweater crafted from smooth refined Merino wool. Essential for layering when changing climates.", 210, "products/18merino.jpg", false, 5),
                new Product(19, men, "French cuff cotton twill oxford", "Made with wrinkle resistant cotton twill, this French-cuffed luxury dress shirt is perfect for Business Class frequent flyers.", 190, "products/19french.jpg"),
                new Product(20, men, "Slim fit dobby oxford shirt", "A bold hue and understated dobby detail bring refined nuance to this modern dress shirt.", 140, "products/20slimfit.jpg"),
                new Product(21, men, "Plaid cotton shirt", "This everyday shirt is equally stylish with jeans or trousers.", 160, "products/21plaid.jpg"),
                new Product(22, men, "Khaki bowery chino pants", "The slim and trim Bowery is a wear-to-work pant you'll actually want to wear. A clean style in our crisp, compact cotton twill, it's perfectly polished (but also comfortable enough for hanging out after hours).", 140, "products/22khaki.jpg"),
                new Product(23, men, "Chealsea tee", "Minimalist style and maximum comfort meet in this lightweight tee", 95, "products/23chealsea.jpg", true, 20),
                new Product(24, men, "Linen blazer", "In airy lightweight linen, this blazer is classic tailoring with a warm weather twist.", 455, "products/24linen.jpg"),
                new Product(25, accessories, "Swiss movement sports watch", "A traditional timepiece with edgy detailing", 500, "products/25swissmovement.jpg"),
                new Product(26, accessories, "Swing time earrings", "Artisans from nonprofit Comite Artisanal Haitien in Port-au-Prince fashion these tasteful earrings from shaped horn. Each pair possesses its own unique natural beauty.", 110, "products/26swing.jpg"),
                new Product(27, accessories, "Silver desert necklace", "Wear your passport by adding an edgy and artistic statement necklace. Ethnic design on hand-hammered and chiseled silver", 210, "products/27silver.jpg"),
                new Product(28, accessories, "Pearl stud earrings", "Prim and demure, pearl studs are a cross cultural symbol of style and refinement.", 110, "products/28pearl.jpg"),
                new Product(29, accessories, "Pearl necklace set", "Fresh Water Pearl Necklaces.", 250, "products/29pearl.jpg", false, 10),
                new Product(30, accessories, "Blue horizons bracelets", "Add a pop of color with these handmade bangles from India", 55, "products/30blue.jpg", true, 10),
                new Product(31, accessories, "Retro chic eyeglasses", "Madison Island Retro chic lenses are stylish on both men and women.", 295, "products/31retro.jpg", false, 10),
                new Product(32, accessories, "Jackie o round sunglasses", "These distinct, feminine frames balance a classic Jackie-O styling with a modern look", 225, "products/32jackie.jpg"),
                new Product(33, accessories, "Aviator sunglasses", "A timeless accessory staple the unmistakable teardrop lenses of our Aviator sunglasses appeal to everyone from suits to rock stars to citizens of the world", 295, "products/33aviator.jpg"),
                new Product(34, homeDecor, "Stone salt and pepper shakers", "A subtle nod to Old World antiquity.", 65, "products/34stone.jpg"),
                new Product(35, homeDecor, "Vase set", "Murray modern vase set. Nice decoration for your rooms.", 135, "products/35vase.jpg", true, 10),
                new Product(36, homeDecor, "Pillow and throw set", "A conveniently packaged pairing of our pillows and throws.", 210, "products/36pillow.jpg"),
                new Product(37, homeDecor, "Park row throw", "A rustic wool blend leaves our Park Row Throw feeling lofty and warm. Packs perfectly into carry-ons.", 120, "products/37park.jpg"),
                new Product(38, homeDecor, "Gramercy throw", "Wrap yourself in this incredibly soft and luxurious blanket for all climate comfort.", 275, "products/38gramercy.jpg", false, 5),
                new Product(39, homeDecor, "Fragance diffuser reeds", "A clean and effective delivery of continuous flameless fragrance to enhance your home.", 75, "products/39fragance.jpg"),
                new Product(40, homeDecor, "Carnegie alpaca throw", "A luxuriously soft throw made of long-fiber lambs wool woven into a Chevron twill.", 275, "products/40carnegie.jpg"),
            };
        }

        public static User[] GetUsers()
        {
            return new[]
            {
                new User()
                {
                    Id = 1, Name = "Store admin", Address = "Western Ave, 22", City = "Birmingham", State="Alabama", Country = "USA",
                    ZipCode = "35005", Email = "info@blazorstore.com", 
                    PasswordHash = "ece20843c3d35a1a2addc40ea5249d040b93eda2840ea586877bd9646c91a2e1", // Hash for password "Blazor1234!",
                    CreationDate = DateTime.Now.AddHours(-1),
                    IsAdmin = true
                },
                new User()
                {
                    Id = 2, Name = "John Smith", Address = "Mayfield Ave, 123", City = "Seattle",  State = "Washington", Country = "USA",
                    ZipCode = "98101", Email = "johnsmith@myserver.com",
                    CreationDate = DateTime.Now,
                    PasswordHash = "ece20843c3d35a1a2addc40ea5249d040b93eda2840ea586877bd9646c91a2e1", // Hash for password "Blazor1234!"
                },
            };
        }
    }
}
