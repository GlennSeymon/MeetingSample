using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingSample.WebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingCategories",
                columns: table => new
                {
                    MeetingCategoryCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingCategories", x => x.MeetingCategoryCode);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DescShort = table.Column<string>(nullable: true),
                    DescLong = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateCode);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StateCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueCode);
                    table.ForeignKey(
                        name: "FK_Venues_States_StateCode",
                        column: x => x.StateCode,
                        principalTable: "States",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    MeetCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    StateCode = table.Column<int>(nullable: true),
                    MeetDate = table.Column<DateTime>(nullable: false),
                    VenueCode = table.Column<int>(nullable: true),
                    MeetingCategoryCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.MeetCode);
                    table.ForeignKey(
                        name: "FK_Meetings_MeetingCategories_MeetingCategoryCode",
                        column: x => x.MeetingCategoryCode,
                        principalTable: "MeetingCategories",
                        principalColumn: "MeetingCategoryCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_States_StateCode",
                        column: x => x.StateCode,
                        principalTable: "States",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Venues_VenueCode",
                        column: x => x.VenueCode,
                        principalTable: "Venues",
                        principalColumn: "VenueCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeetingMeetCode = table.Column<int>(nullable: true),
                    RaceNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Distance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceCode);
                    table.ForeignKey(
                        name: "FK_Races_Meetings_MeetingMeetCode",
                        column: x => x.MeetingMeetCode,
                        principalTable: "Meetings",
                        principalColumn: "MeetCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MeetingCategories",
                columns: new[] { "MeetingCategoryCode", "Description" },
                values: new object[,]
                {
                    { 1, "Professional" },
                    { 2, "Picnic" },
                    { 3, "Point to Point" },
                    { 4, "Trail" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateCode", "DescLong", "DescShort" },
                values: new object[,]
                {
                    { 1, "New South Wales", "NSW" },
                    { 2, "Northern Territory", "NT" },
                    { 3, "Queensland", "QLD" },
                    { 4, "South Australia", "SA" },
                    { 5, "Tasmania", "TAS" },
                    { 6, "Victoria", "VIC" },
                    { 7, "Western Australia", "WA" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueCode", "Name", "StateCode" },
                values: new object[,]
                {
                    { 1, "Adaminaby", 1 },
                    { 297, "Penola", 4 },
                    { 291, "Oakbank", 4 },
                    { 274, "Naracoorte", 4 },
                    { 268, "Murray Bridge", 4 },
                    { 259, "Mount Gambier", 4 },
                    { 253, "Morphettville", 4 },
                    { 241, "Mindarie-Halidon", 4 },
                    { 220, "Lock", 4 },
                    { 213, "Laura (SA)", 4 },
                    { 207, "Kingscote", 4 },
                    { 204, "Kimba", 4 },
                    { 298, "Penong", 4 },
                    { 190, "Jamestown", 4 },
                    { 145, "Gawler", 4 },
                    { 84, "Clare", 4 },
                    { 81, "Cheltenham Park", 4 },
                    { 77, "Ceduna", 4 },
                    { 48, "Bordertown", 4 },
                    { 36, "Berri", 4 },
                    { 19, "Balaklava", 4 },
                    { 451, "Cairns Night", 3 },
                    { 445, "Toowoomba Inner Track", 3 },
                    { 439, "Sunshine Coast@Inner Track", 3 },
                    { 438, "Rockhampton Sand Track", 3 },
                    { 173, "Hawker", 4 },
                    { 436, "Toowoomba Cushion Track", 3 },
                    { 305, "Port Augusta", 4 },
                    { 309, "Port Pirie", 4 },
                    { 34, "Benalla", 6 },
                    { 22, "Balnarring", 6 },
                    { 20, "Sportsbet-Ballarat", 6 },
                    { 18, "Bairnsdale", 6 },
                    { 17, "Avoca", 6 },
                    { 10, "Ararat", 6 },
                    { 5, "Alexandra", 6 },
                    { 442, "Devonport Tapeta Synthetic", 5 },
                    { 437, "Hobart Inside Track", 5 },
                    { 330, "Spreyton", 5 },
                    { 264, "Launceston", 5 },
                    { 307, "Port Lincoln", 4 },
                    { 222, "Longford", 5 },
                    { 132, "Hobart", 5 },
                    { 117, "Devonport", 5 },
                    { 115, "Deloraine", 5 },
                    { 443, "Millicent", 4 },
                    { 435, "Morphettville Parks", 4 },
                    { 376, "Victoria Park", 4 },
                    { 373, "Tumby Bay", 4 },
                    { 340, "Streaky Bay", 4 },
                    { 339, "Strathalbyn", 4 },
                    { 324, "Roxby Downs", 4 },
                    { 316, "Quorn", 4 },
                    { 206, "King Island", 5 },
                    { 434, "Sunshine Coast Cushion Track", 3 },
                    { 424, "Deagon", 3 },
                    { 421, "Bowen River", 3 },
                    { 334, "Stamford", 3 },
                    { 333, "St George", 3 },
                    { 331, "Springsure", 3 },
                    { 328, "Sedan Dip", 3 },
                    { 322, "Roma", 3 },
                    { 321, "Rockhampton", 3 },
                    { 320, "Ridgelands", 3 },
                    { 319, "Richmond", 3 },
                    { 314, "Quilpie", 3 },
                    { 312, "Quamby", 3 },
                    { 310, "Prairie", 3 },
                    { 335, "Stanthorpe", 3 },
                    { 300, "Pentland", 3 },
                    { 292, "Oakey", 3 },
                    { 290, "Oak Park", 3 },
                    { 285, "Normanton", 3 },
                    { 283, "Noorama", 3 },
                    { 282, "Noccundra", 3 },
                    { 273, "Nanango", 3 },
                    { 272, "Muttaburra", 3 },
                    { 263, "Mount Perry", 3 },
                    { 261, "Mount Isa", 3 },
                    { 260, "Mount Garnet", 3 },
                    { 256, "Morven", 3 },
                    { 293, "Oakley", 3 },
                    { 337, "Stonehenge", 3 },
                    { 341, "Surat", 3 },
                    { 346, "Talwood", 3 },
                    { 419, "Laura (Q)", 3 },
                    { 418, "Hebel", 3 },
                    { 417, "Sunshine Coast", 3 },
                    { 416, "Camboon", 3 },
                    { 415, "Arrilalah", 3 },
                    { 412, "Yeppoon", 3 },
                    { 408, "Yaraka", 3 },
                    { 404, "Wyandra", 3 },
                    { 402, "Wondai", 3 },
                    { 400, "Winton", 3 },
                    { 399, "Windorah", 3 },
                    { 397, "Wilpeena", 3 },
                    { 389, "Warwick", 3 },
                    { 385, "Warra", 3 },
                    { 382, "Wandoan", 3 },
                    { 375, "Twin Hills", 3 },
                    { 366, "Townsville", 3 },
                    { 365, "Tower Hill", 3 },
                    { 363, "Toowoomba", 3 },
                    { 358, "Theodore", 3 },
                    { 356, "Thangool", 3 },
                    { 355, "Texas", 3 },
                    { 351, "Taroom", 3 },
                    { 349, "Tara", 3 },
                    { 347, "Tambo", 3 },
                    { 35, "Bendigo", 6 },
                    { 250, "Moranbah", 3 },
                    { 57, "Buchan", 6 },
                    { 67, "Camperdown", 6 },
                    { 139, "Exmouth", 7 },
                    { 137, "Esperance Bay", 7 },
                    { 127, "Landor", 7 },
                    { 120, "Dongara-Irwin", 7 },
                    { 111, "Derby", 7 },
                    { 94, "Collie", 7 },
                    { 72, "Carnarvon", 7 },
                    { 58, "Bunbury", 7 },
                    { 56, "Broome", 7 },
                    { 39, "Beverley", 7 },
                    { 33, "Belmont", 7 },
                    { 140, "Fitzroy", 7 },
                    { 14, "Ashburton", 7 },
                    { 3, "Albany", 7 },
                    { 452, "Racing.com Park", 6 },
                    { 449, "Nine Dragons", 6 },
                    { 448, "Racing.com Park Synthetic", 6 },
                    { 441, "Cranbourne-Syn", 6 },
                    { 433, "bet365 Geelong Synthetic", 6 },
                    { 430, "Wycheproof", 6 },
                    { 429, "Ladbrokes Park Lakeside", 6 },
                    { 428, "Ladbrokes Park Hillside", 6 },
                    { 425, "Cranbourne-Trn", 6 },
                    { 423, "Yarra Glen Hunt", 6 },
                    { 13, "Ascot", 7 },
                    { 411, "Yea", 6 },
                    { 149, "Geraldton", 7 },
                    { 197, "Kalgoorlie", 7 },
                    { 407, "Yalgoo", 7 },
                    { 405, "Wyndham", 7 },
                    { 398, "Wiluna", 7 },
                    { 362, "Toodyay", 7 },
                    { 306, "Port Hedland", 7 },
                    { 303, "Pinjarra Park", 7 },
                    { 302, "Pingrup", 7 },
                    { 287, "Northam", 7 },
                    { 286, "Norseman", 7 },
                    { 284, "Roebourne", 7 },
                    { 280, "Newman", 7 },
                    { 195, "Junction", 7 },
                    { 277, "Narrogin", 7 },
                    { 258, "Mount Barker", 7 },
                    { 249, "Moora", 7 },
                    { 243, "Mingenew- Yandanooka", 7 },
                    { 234, "Meekatharra", 7 },
                    { 229, "Marble Bar", 7 },
                    { 217, "Leonora", 7 },
                    { 216, "Leinster", 7 },
                    { 214, "Laverton", 7 },
                    { 210, "Kununurra", 7 },
                    { 208, "Kojonup", 7 },
                    { 205, "Kimberley", 7 },
                    { 262, "Mount Magnet", 7 },
                    { 409, "Yarra Valley", 6 },
                    { 403, "Woolamai", 6 },
                    { 401, "Wodonga", 6 },
                    { 227, "Manangatang", 6 },
                    { 211, "bet365 Park Kyneton", 6 },
                    { 203, "bet365 Park Kilmore", 6 },
                    { 201, "Kerang", 6 },
                    { 181, "Horsham", 6 },
                    { 178, "Hinnomunjie", 6 },
                    { 176, "Healesville", 6 },
                    { 171, "Hanging Rock", 6 },
                    { 170, "Hamilton", 6 },
                    { 166, "Gunbower", 6 },
                    { 160, "Great Western", 6 },
                    { 228, "Mansfield", 6 },
                    { 147, "bet365 Geelong", 6 },
                    { 129, "Edenhope", 6 },
                    { 128, "Echuca", 6 },
                    { 125, "Dunkeld", 6 },
                    { 122, "Drouin", 6 },
                    { 119, "Donald", 6 },
                    { 113, "Dederang", 6 },
                    { 107, "Cranbourne", 6 },
                    { 92, "Coleraine", 6 },
                    { 91, "Colac", 6 },
                    { 76, "Caulfield", 6 },
                    { 75, "Casterton", 6 },
                    { 141, "Flemington", 6 },
                    { 237, "Merton", 6 },
                    { 239, "Mildura", 6 },
                    { 246, "Moe", 6 },
                    { 396, "Werribee Park", 6 },
                    { 395, "Werribee", 6 },
                    { 388, "Warrnambool", 6 },
                    { 386, "Warracknabeal", 6 },
                    { 383, "Wangaratta", 6 },
                    { 369, "Traralgon", 6 },
                    { 367, "Towong", 6 },
                    { 357, "The Kennels", 6 },
                    { 354, "Terang", 6 },
                    { 352, "Tatura", 6 },
                    { 343, "Swifts Creek", 6 },
                    { 342, "Swan Hill", 6 },
                    { 338, "Stony Creek", 6 },
                    { 336, "Stawell", 6 },
                    { 332, "St Arnaud", 6 },
                    { 329, "Seymour", 6 },
                    { 326, "Sandown", 6 },
                    { 325, "Sale", 6 },
                    { 299, "Penshurst", 6 },
                    { 295, "Pakenham", 6 },
                    { 281, "Nhill", 6 },
                    { 269, "Murtoa", 6 },
                    { 254, "Mortlake", 6 },
                    { 252, "Mornington", 6 },
                    { 248, "Moonee Valley", 6 },
                    { 63, "Burrumbeet", 6 },
                    { 413, "York", 7 },
                    { 247, "Monto", 3 },
                    { 242, "Mingela", 3 },
                    { 267, "Mungindi", 1 },
                    { 266, "Mungery", 1 },
                    { 265, "Mudgee", 1 },
                    { 257, "Moulamein", 1 },
                    { 255, "Moruya", 1 },
                    { 251, "Moree", 1 },
                    { 245, "Moama", 1 },
                    { 236, "Merriwa", 1 },
                    { 235, "Mendooran", 1 },
                    { 226, "Mallawa", 1 },
                    { 224, "Louth", 1 },
                    { 270, "Murwillumbah", 1 },
                    { 221, "Lockhart", 1 },
                    { 218, "Lightning Ridge", 1 },
                    { 215, "Leeton", 1 },
                    { 200, "Kempsey", 1 },
                    { 199, "Kembla Grange", 1 },
                    { 193, "Jerilderie", 1 },
                    { 187, "Inverell", 1 },
                    { 179, "Holbrook", 1 },
                    { 177, "Hillston", 1 },
                    { 175, "Hay", 1 },
                    { 174, "Hawkesbury", 1 },
                    { 172, "Harden", 1 },
                    { 219, "Lismore", 1 },
                    { 168, "Gunnedah", 1 },
                    { 271, "Muswellbrook", 1 },
                    { 276, "Narrandera", 1 },
                    { 371, "Tullamore", 1 },
                    { 370, "Trundle", 1 },
                    { 368, "Trangie", 1 },
                    { 364, "Tottenham", 1 },
                    { 361, "Tomingley", 1 },
                    { 360, "Tocumwal", 1 },
                    { 350, "Taree", 1 },
                    { 348, "Tamworth", 1 },
                    { 345, "Talmoi", 1 },
                    { 344, "Tabulam", 1 },
                    { 327, "Scone", 1 },
                    { 275, "Narrabri", 1 },
                    { 323, "Rosehill Gardens", 1 },
                    { 315, "Quirindi", 1 },
                    { 313, "Queanbeyan", 1 },
                    { 311, "Quambone", 1 },
                    { 308, "Port Macquarie", 1 },
                    { 304, "Pooncarie", 1 },
                    { 296, "Parkes", 1 },
                    { 294, "Orange", 1 },
                    { 289, "Nyngan", 1 },
                    { 288, "Nowra", 1 },
                    { 279, "Newcastle", 1 },
                    { 278, "Narromine", 1 },
                    { 317, "Royal Randwick", 1 },
                    { 167, "Gundagai", 1 },
                    { 165, "Gulgong", 1 },
                    { 164, "Gulargambone", 1 },
                    { 71, "Carinda", 1 },
                    { 69, "Canterbury Park", 1 },
                    { 68, "Canberra", 1 },
                    { 60, "Bundarra", 1 },
                    { 55, "Broken Hill", 1 },
                    { 54, "Brewarrina", 1 },
                    { 53, "Braidwood", 1 },
                    { 52, "Bowraville", 1 },
                    { 50, "Bourke", 1 },
                    { 47, "Boorowa", 1 },
                    { 46, "Bong Bong", 1 },
                    { 73, "Carrathool", 1 },
                    { 45, "Bombala", 1 },
                    { 40, "Bingara", 1 },
                    { 37, "Berrigan", 1 },
                    { 30, "Bedgerebong", 1 },
                    { 28, "Bathurst", 1 },
                    { 26, "Barraba", 1 },
                    { 24, "Baradine", 1 },
                    { 23, "Balranald", 1 },
                    { 21, "Ballina", 1 },
                    { 12, "Armidale", 1 },
                    { 11, "Ardlethan", 1 },
                    { 4, "Albury", 1 },
                    { 41, "Binnaway", 1 },
                    { 74, "Casino", 1 },
                    { 78, "Cessnock", 1 },
                    { 88, "Cobar", 1 },
                    { 163, "Griffith", 1 },
                    { 162, "Grenfell", 1 },
                    { 159, "Grafton", 1 },
                    { 158, "Goulburn", 1 },
                    { 157, "Gosford", 1 },
                    { 153, "Glen Innes", 1 },
                    { 151, "Gilgandra", 1 },
                    { 150, "Geurie", 1 },
                    { 143, "Forbes", 1 },
                    { 134, "Enngonia", 1 },
                    { 124, "Dubbo", 1 },
                    { 116, "Deniliquin", 1 },
                    { 114, "Deepwater", 1 },
                    { 108, "Crookwell", 1 },
                    { 106, "Cowra", 1 },
                    { 105, "Corowa", 1 },
                    { 103, "Cootamundra", 1 },
                    { 102, "Coonamble", 1 },
                    { 101, "Coonabarabran", 1 },
                    { 100, "Cooma", 1 },
                    { 99, "Coolabah", 1 },
                    { 97, "Condobolin", 1 },
                    { 96, "Come-By-Chance", 1 },
                    { 93, "Collarenebri", 1 },
                    { 90, "Coffs Harbour", 1 },
                    { 372, "Tumbarumba", 1 },
                    { 244, "Mitchell", 3 },
                    { 374, "Tumut", 1 },
                    { 378, "Walcha", 1 },
                    { 146, "Gayndah", 3 },
                    { 144, "Gatton", 3 },
                    { 142, "Flinton", 3 },
                    { 138, "Ewan", 3 },
                    { 136, "Esk", 3 },
                    { 135, "Eromanga", 3 },
                    { 133, "Emerald", 3 },
                    { 131, "Einasleigh", 3 },
                    { 130, "Eidsvold", 3 },
                    { 126, "Eagle Farm", 3 },
                    { 123, "Duaringa", 3 },
                    { 148, "Georgetown", 3 },
                    { 121, "Doomben", 3 },
                    { 110, "Dalby", 3 },
                    { 109, "Cunnamulla", 3 },
                    { 104, "Corfield", 3 },
                    { 98, "Cooktown", 3 },
                    { 95, "Collinsville", 3 },
                    { 89, "Coen", 3 },
                    { 87, "Cloncurry", 3 },
                    { 86, "Clifton", 3 },
                    { 85, "Clermont", 3 },
                    { 83, "Chinchilla", 3 },
                    { 82, "Chillagoe", 3 },
                    { 118, "Dingo", 3 },
                    { 80, "Charters Towers", 3 },
                    { 152, "Gladstone", 3 },
                    { 155, "Goondiwindi", 3 },
                    { 240, "Miles", 3 },
                    { 238, "Middlemount", 3 },
                    { 233, "McKinlay", 3 },
                    { 232, "Maxwelton", 3 },
                    { 230, "Mareeba", 3 },
                    { 225, "Mackay", 3 },
                    { 223, "Longreach", 3 },
                    { 209, "Kumbia", 3 },
                    { 202, "Kilcoy", 3 },
                    { 196, "Jundah", 3 },
                    { 194, "Julia Creek", 3 },
                    { 154, "Gold Coast", 3 },
                    { 192, "Jericho", 3 },
                    { 189, "Isisford", 3 },
                    { 188, "Ipswich", 3 },
                    { 186, "Innisfail", 3 },
                    { 185, "Injune", 3 },
                    { 184, "Ingham", 3 },
                    { 183, "Ilfracombe", 3 },
                    { 182, "Hughenden", 3 },
                    { 180, "Home Hill", 3 },
                    { 169, "Gympie", 3 },
                    { 161, "Gregory Downs", 3 },
                    { 156, "Gordonvale", 3 },
                    { 191, "Jandowae", 3 },
                    { 79, "Charleville", 3 },
                    { 70, "Capella", 3 },
                    { 66, "Camooweal", 3 },
                    { 6, "Ladbrokes Pioneer Park", 2 },
                    { 2, "Adelaide River", 2 },
                    { 450, "Beaumont Newcastle", 1 },
                    { 447, "Wagga Riverside", 1 },
                    { 446, "Fernhill", 1 },
                    { 444, "Hawkesbury Inner", 1 },
                    { 440, "Tullibigeal", 1 },
                    { 432, "Canberra Acton", 1 },
                    { 431, "Tuncurry", 1 },
                    { 426, "Kensington", 1 },
                    { 422, "Sapphire Coast", 1 },
                    { 27, "Barrow Creek", 2 },
                    { 414, "Young", 1 },
                    { 406, "Wyong", 1 },
                    { 394, "Wentworth", 1 },
                    { 393, "Wellington", 1 },
                    { 392, "Wean", 1 },
                    { 391, "Wauchope", 1 },
                    { 390, "Warwick Farm", 1 },
                    { 387, "Warren", 1 },
                    { 384, "Warialda", 1 },
                    { 381, "Wamboyne", 1 },
                    { 380, "Wallabadah", 1 },
                    { 379, "Walgett", 1 },
                    { 410, "Yass", 1 },
                    { 112, "Darwin", 2 },
                    { 198, "Katherine", 2 },
                    { 212, "Larrimah", 2 },
                    { 65, "Calliope", 3 },
                    { 64, "Cairns", 3 },
                    { 62, "Burrandowan", 3 },
                    { 61, "Burketown", 3 },
                    { 59, "Bundaberg", 3 },
                    { 51, "Bowen", 3 },
                    { 49, "Boulia", 3 },
                    { 44, "Bluff", 3 },
                    { 43, "Blackall", 3 },
                    { 42, "Birdsville", 3 },
                    { 38, "Betoota", 3 },
                    { 32, "Bell", 3 },
                    { 31, "Bedourie", 3 },
                    { 29, "Beaudesert", 3 },
                    { 25, "Barcaldine", 3 },
                    { 16, "Augathella", 3 },
                    { 15, "Atherton", 3 },
                    { 9, "Aramac", 3 },
                    { 8, "Alpha", 3 },
                    { 7, "Almaden", 3 },
                    { 359, "Timber Creek", 2 },
                    { 353, "Tennant Creek", 2 },
                    { 318, "Renner", 2 },
                    { 301, "Pine Creek", 2 },
                    { 231, "Mataranka", 2 },
                    { 377, "Wagga", 1 },
                    { 427, "Lark Hill", 7 }
                });

            migrationBuilder.InsertData(
                table: "Meetings",
                columns: new[] { "MeetCode", "MeetDate", "MeetingCategoryCode", "StateCode", "Title", "VenueCode" },
                values: new object[,]
                {
                    { 2, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, "Meeting 2", 76 },
                    { 4, new DateTime(2018, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, "Meeting 4", 76 },
                    { 1, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, "Meeting 1", 141 },
                    { 3, new DateTime(2018, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, "Meeting 3", 141 },
                    { 5, new DateTime(2018, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, "Meeting 5", 141 }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "RaceCode", "Distance", "MeetingMeetCode", "Name", "RaceNumber" },
                values: new object[,]
                {
                    { 4, 1000, 2, "Meeting 2, Race 1", 1 },
                    { 5, 1200, 2, "Meeting 2, Race 2", 2 },
                    { 6, 1300, 2, "Meeting 2, Race 3", 3 },
                    { 7, 1000, 2, "Meeting 2, Race 4", 4 },
                    { 1, 1000, 1, "Meeting 1, Race 1", 1 },
                    { 2, 1200, 1, "Meeting 1, Race 2", 2 },
                    { 3, 1300, 1, "Meeting 1, Race 3", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_MeetingCategoryCode",
                table: "Meetings",
                column: "MeetingCategoryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_StateCode",
                table: "Meetings",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_VenueCode",
                table: "Meetings",
                column: "VenueCode");

            migrationBuilder.CreateIndex(
                name: "IX_Races_MeetingMeetCode",
                table: "Races",
                column: "MeetingMeetCode");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_StateCode",
                table: "Venues",
                column: "StateCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "MeetingCategories");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
