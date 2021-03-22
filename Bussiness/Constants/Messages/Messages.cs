using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Constants.Messages
{
    public static class Messages
    {
        public static string UserCantDeleted="Kullanıcı silinemedi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string UserListed = "Kullanıcılar listelendi";
        public static string UserByCustomerId = "Müşteriye göre kullanıcı getirildi";
        public static string GetByUserId = "Kullanıcı getirildi";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserUpdated = "Kullanıcı Güncellendi";
        public static string UserCantUpdated = "Kullanıcı Güncellenemedi";
        public static string UserCantAdded = "Kullanıcı Eklenemedi";


        // For Car

        public static string CarAdded = "Araba Eklendi";
        public static string CarNotAdded = "Araba Eklenemedi. Araba ismi geçersiz.";
        public static string CarUpdated = "Araba Güncellendi";
        public static string CarNotUpdated = "Araba Güncellenemedi";
        public static string CarDeleted = "Araba Silindi";
        public static string CarNotDeleted = "Araba Silinemedi";
        public static string CarCantFind = "güncellenecek Araba bulunamadı";

        public static string CarListed = "Arabalar Listelendi";
        public static string CarNotListed = "Arabalar Listelenemedi";
        public static string CarByID = "Araba Getirildi";
        public static string NotCarByID = "Araba Getirilemedi";
        public static string CarByColorID = "Renge göre Arabalar Listelendi";
        public static string NotCarByColorID = "Renge göre Arabalar Listelenemedi";
        public static string CarByBrandID = "Markaya göre Arabalar Listelendi";
        public static string NotCarByBrandID = "Markaya göre Arabalar Listelenemedi";
        public static string CarDetailList = "Araba Detay Listesi";
        public static string NotCarDetailList = "Araba Detay Listesi Getirilemedi";
        public static string CarDailyPrice = "Günlük ücreti sıfırdan büyük girmelisiniz";

        // For Color
        public static string ColorAdded = "Renk Eklendi";
        public static string ColorNotAdded = "Renk Eklenemedi";
        public static string ColorUpdated = "Renk Güncellendi";
        public static string ColorNotUpdated = "Renk Güncellenemedi";
        public static string ColorDeleted = "Renk Silindi";
        public static string ColorNotDeleted = "Renk Silinemedi";
        public static string ColorListed = "Renkler listelendi";
        public static string ColorNotListed = "Renkler listelenemedi";
        public static string ColorByID = "Renk Getirildi";
        public static string NotColorByID = "Renk Getirilemedi";
        public static string ColorAlreadyExist = "Renk zaten var";


        // For Brand

        public static string BrandAdded = "Marka Eklendi";
        public static string BrandNotAdded = "Marka Eklenemedi";
        public static string BrandUpdated = "Marka Güncellendi";
        public static string BrandNotUpdated = "Marka Güncellenemedi";
        public static string BrandDeleted = "Marka Silindi";
        public static string BrandNotDeleted = "Marka Silinemedi";
        public static string BrandListed = "Markalar listelendi";
        public static string BrandNotListed = "Markalar listelenemedi";
        public static string BrandByID = "Marka Getirildi";
        public static string NotBrandByID = "Marka Getirilemedi";
        public static string BrandAllreadyExist = "Marka zaten ekli";


        // genel

        public static string MaintanceTime = "Sistem kapalı, bakımda";


        // user 
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "şifre bulunamadı ";
        public static string SuccessfullLogin = "Sisteme giriş başarılı ";
        public static string UserExist = "Kullanıcı mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";


        // rental 

        public static string RentalListed = "Kiralık Arabalar Listesi";
        public static string RentalById = "kiralık araç getirildi.";
        public static string RentalAdded = "Araç Kiralandı";
        public static string RentalNotAdded = "Araç Kiralanamadı";
        public static string RentalNotDeleted = "Kiralık araç silinemedi";
        public static string RentalDeleted = "Kiralık araç silindi";
        public static string RentalNotUpdated = "Kiralık araç Güncellenemedi";
        public static string RentalUpdated = "Kiralık araç Güncellendi";
        public static string CarAlreadyRented = "Araba hala kiralık";

        // customer

        public static string CustomerListed = "Müşteriler Listelendi";
        public static string CustomerById = "Müşteri getirildi";
        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomerNotDeleted = "Müşteri silinemedi";
        public static string CustomerDeleted = "Müşteri silindi";
        public static string CustomerNotUpdated = "Müşteri güncellenemedi";
        public static string CustomerUpdated = "Müşteri güncellendi";
        public static string CustomerNotAdded = "Müşteri eklenemedi";


        // id'sel
        public static string IdNotFound = "id hatalı ya da yanlış geldi";


        // car İmage
        public static string ImageListed = "Resimler Listelendi";
        public static string ListedByCarId = "Aracın resimleri getirildi.";
        public static string CarImage = "Araç resmi getirildi.";
        public static string ImageAdded = "Araç resmi eklendi.";
        public static string ImageUpdated = "Araç resmi güncellendi.";
        public static string ImageDeleted = "Araç resmi silindi..";

        public static string OverCountCarImage= "bir aracın en fazla 5 resmi olabilir";
        public static string CarImageDefault="Arabaya ait resim bulunamamaktadır.";


        // authorization
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string AccessTokenCreated = "Token üretildi";

        public static string CustomerDetailListed = "Müşteriler listelendi";
        public static string CustomerNotListed = "Müşteriler Listelenemedi";

        public static string RentalDetailListed = "Kira Detay Listelendi";

        public static string CarByBrandAndCar = "Arabalar İki yönlü Filtrelendi";
    }
}
