using System;
using System.Collections.Generic;
using System.Text;

namespace TravelGuide.Helpers
{
	public static class IconFont
    {
		public const string Account = "\U000f0004"; //Потребител, човече
		public const string AccountTie = "\U000f0ce3"; // Човече с вратовръзка
		public const string AccountCircleOutline = "\U000f0b55"; //Потребител прозрачен
		public const string Storefront = "\U000f07c7"; //Магазин/обект
		public const string WidgetsOutline = "\U000f1355"; //Джаджи, квадратчета 
		public const string TagMultipleOutline = "\U000f12f7"; //Таг
		public const string ArchiveOutline = "\U000f120e"; //Архив/Кутия 
		public const string ArrowRightBold = "\U000f0734"; //стрелка надясно 
		public const string Home = "\U000f02dc"; //къщичка/HomеPage
		public const string CogOutline = "\U000f08bb"; //Прозрачни настройки
		public const string Cogs = "\U000f08d6"; //Настройки
		public const string Logout = "\U000f0343"; //Изход
		public const string Login = "\U000f0342"; //Вход
		public const string FolderAccount = "\U000f024c"; //Папка с потребител
		public const string Folder = "\U000f024b"; //Папка
		public const string Database = "\U000f01bc"; //база данни
		public const string File = "\U000f0214";
		public const string NoteOutline = "\U000f039b";
		public const string Note = "\U000f039a";
		public const string Printer = "\U000f042a"; //принтер
		public const string Cog = "\U000f0493";
		public const string PackageVariantClosed = "\U000f03d7";
		public const string FileCheckOutline = "\U000f0e29";
		public const string BookCheckOutline = "\U000f14f4";
		public const string BookOpen = "\U000f00bd";//справки
		public const string Plus = "\U000f0415";//плюсче(добавяне на нов партньор)
		public const string AccountSearchOutline = "\U000f0935";//лупичка с човече(търсене по ЕИК)
		public const string BarcodeScan = "\U000f0072";//баркод скенер
		public const string Delete = "\U000f01b4";
		public const string Edit = "\U000f0dc8";
		public const string FileDocumentOutline = "\U000f09ee";//документ който се вижда в списъка с опрации - раздел "Фактуриране"
		public const string Minus = "\U000f0374";// минусче
		public const string QrcodeScan = "\U000f0433";//QR код
		public const string CheckboxMarkedCircleOutline = "\U000f0134";//икона, указваща че дадената операция има издадена фактура - раздел "Фактуриране"
		public const string CloseCircleOutline = "\U000f015a";//икона, указваща че дадената операция няма издадена фактура - раздел "Фактуриране"
		public const string SaveSettings = "\U000f145c";//икона дискета запис
		public const string PlusBoxMultiple = "\U000f0334";//Добавя повече от 1 количество за стока
		public const string FilePlus = "\U000f0752";//Добавя стока към операцията
		public const string ContentSaveOutline = "\U000f0818";//Икона запис на плащане
		public const string PrinterPos = "\U000f1057";//Принтер - икона за допълнителни операции с КА
		public const string BriefcaseSearchOutline = "\U000f0c42";//търсена на стока по баркод
		public const string LockCheck = "\U000f139a";//катинарче
		public const string Eye = "\U000f0208";//око за поле за парола
		public const string EyeOff = "\U000f0209";//затворено око за поле за парола
		public const string Server = "\U000f048b";//сървър
		public const string Warehouse = "\U000f0f81";//склад-екран настройки
		public const string AccountGroup = "\U000f0849";//групи партньори-екран настройки
		public const string PackageVariant = "\U000f03d6";//отворен кашон-екран настройки
		public const string Buffer = "\U000f0619";//режим на работа
		public const string AccountPlus = "\U000f0014";//нов партньор
		public const string PlusBox = "\U000f0416";//нова стока
		public const string Sync = "\U000f04e6";//синхронизация
		public const string Magnify = "\U000F0349";//лупа
		public const string Chevronup = "\U000f0143";//стрелка нагоре
		public const string Chevrondown = "\U000f0140"; //стрелка надолу
		public const string Calculator = "\U000f00ec";
		public const string PriceRules = "\U000f081e"; //стрелка надолу
		public const string AddQttyUpToTen = "\u0056";//увеличаване на количество на стока, до такова, кратно на 10
		public const string AddQttyUpToSecondMeasure = "\u0057";//увеличаване на количество на стока, до такова, кратно на втора мертна единица
		public const string AddQttyUpToOneHundred = "\u0058";//увеличаване на количество на стока, до такова, кратно на 100
		public const string Filter = "\U000F0232"; //филтър

		//В меню покажи още
		public const string InformationOutline = "\U000f02fd";//за програмата
		public const string AdditionalSettings = "\U000f035c";//допълнителни настройки

		public const string MicroinvestLogo = "\u0043";//логото на Микроинвест
		public const string AddGoodToOperation = "\u004a";//икона за добавяне на стока въм операция
		public const string Menu = "\u0042";//меню

		//Икони за начален екран
		public const string OrderIcon = "\u0044";//order
		public const string SaleIcon = "\u0045";//sale
		public const string DeliveryIcon = "\u0046";//delivery	
		public const string RefundIcon = "\u0047";//refund
		public const string PaymentsIcon = "\u0048";//payments
		public const string InvoicingIcon = "\u0049";//invoicing

		public const string Calendar = "\u004c";//календар - сторно
		public const string Watch = "\u004d";//часовник - сторно

		//Икони за раздел справки
		public const string Turnover = "\u004e";//оборот
		public const string Sales = "\u004f";//продажби
		public const string SalesByPartners = "\u0050";//продажби по партньори
		public const string Refund = "\u0051";//сторно
		public const string GoodsАvailability = "\u0052";//наличност на стоки
		public const string ObligationByPartners = "\u0053";//задължения по партньор
		public const string Invoices = "\u0054";//издадени фактури
		public const string PaidAndUnpaidDocuments = "\u0055";//платени и неплатени документи

		//Екран фактуриране
		public const string Tick = "\u0062";
		public const string Ex = "\u0064";

		public const string Landmark = "\U000F034E";
		public const string Image = "\U000F0E09";
	}
}
