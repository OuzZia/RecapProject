using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string Deleted = "Kayıt başarıyla silindi";
        public static string InvalidName = "Araba ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string CarsListed = "Araçlar listelendi";
        public static string Added = "Kayıt başarıyla eklendi";
        public static string InvalidEntity = "Kayıt bulunamadı";
        public static string Updated = "Kayıt başarıyla güncellendi";
        public static string BrandsListed = "Markalar listelendi";
        public static string ColorsListed = "Renkler listelendi";
        public static string InvalidPassword = "Şifre en az 8 karakter olmalı";
        public static string UsersListed = "Kullanıcılar listelendi";
        public static string NotReturned = "Araç henüz dönmedi, kiralanamaz";
        public static string Rented = "Araç kiralama başarılı";
        public static string NotDeleted = "Kayıt silinemedi";
        public static string RentalListed = "Kiralama bilgileri listelendi";
        public static string CustomersListed = "Müşteriler listelendi.";
        public static string CarImagesNotFound = "Araca ait görsel bulunamadı";
        public static string CarImageDeleted = "Araç görseli silindi";
        public static string CarImageUpdated = "Araç görseli güncellendi";
        public static string CarImageAdded = "Araç görseli eklendi";
        public static string CarImageLimitExceeded = "Bu araç için daha fazla görsel eklenemez";
        public static string InvalidDescription = "Araç açıklaması 5 karakterden kısa olamaz";
        public static string InvalidDailyPrice = "Günlük fiyat minimum 250, maksimum 800 olabilir";
        public static string InvalidModelYear = "2 yaşından büyük araç kaydı yapılamaz";
        public static string ReturnDateInvalid = "Araç müşteride";
        public static string ThisCarNotReturned = "Araç müşteride, kiralanamaz";
        public static string ReturnDateCannotBeEarlierThanRentDate = "Araç dönüş tarihi kiralama tarihinden daha önce olamaz";
        public static string ThisCarCanNotBeRentedForThisRentDate = "Bu araç bu tarihlerde müsait değil";
        public static string CarImagesListed = "Araç görselleri listelendi";
        public static string UserRegistered = "Kayıt başarılı";
        public static string UserNotFound = "Kullanıcı kaydı bulunamadı";
        public static string PasswordError = "Hatalı şifre";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Bu email ile başka bir kayıt mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
        public static string AuthorizationDenied = "Yetkiniz yok";
    }
}
