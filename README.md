# SignalR

## SignalR Nedir?

**SignalR**, ASP.NET Core için geliştirilmiş, gerçek zamanlı iletişim sağlayan bir kütüphanedir. SignalR, istemci (client) ve sunucu (server) arasında iki yönlü, düşük gecikmeli iletişim kurar ve aşağıdaki protokolleri otomatik olarak kullanır:

- **WebSockets**
- **Server-Sent Events**
- **Long Polling**

---

## Kullanım Alanları

1. **Gerçek zamanlı sohbet uygulamaları**
2. **Canlı veri akışı (ör. spor skorları, finansal veriler)**
3. **Bildirim sistemleri (toast mesajlar)**
4. **Dosya aktarımı ve işleme**
5. **Çok oyunculu oyunlar (multiplayer gaming)**

---

## Proje Özeti

### Başlıca Özellikler:

1. **Hub İletişimi:**
   - SignalR Hub ile istemciler arasında çift yönlü iletişim.
   - Tür güvenli mesajlaşma (type-safe).
   - Bağlı istemci sayısını izleme.

2. **Gruplar ve Özel İletişim:**
   - Gruplara özel mesajlaşma.
   - Belirli istemciye mesaj gönderme.

3. **Akış (Streaming):**
   - Chunked veri gönderimi ve alımı.

4. **Excel İşleme:**
   - Arka planda Excel dosyası oluşturma ve istemciye anlık bildirim.

---

## Ekran Görüntüleri (SignalR Hub ile istemciler arasındaki etkileşimler)

### 1. SignalR Uygulaması

#### Tüm Client'lara Mesaj Gönderme
client method gelen mesajlar client olarak tüketildi
![Type Safe Hub](https://github.com/user-attachments/assets/aadb5769-d8d1-4c54-8392-aea65d9102d3)

#### OnConnectedAsync() ve OnDisconnectedAsync()
Bağlı client sayısını izleme.
![OnConnectedAsync ve OnDisconnectedAsync](https://github.com/user-attachments/assets/4d3da1f5-af14-4a3b-8aee-4ae46e3d5bf9)

#### Sadece İstek Yapan Client'e Mesaj Gönderme
Caller Client, ReceiveMessageForCallerClient (sadece istek yapan client tetiklenir)
![image](https://github.com/user-attachments/assets/1db8fd2b-343f-4d6a-9053-fdaccc642bfc)

#### Diğer Client'lere Mesaj Gönderme
İstek yapan client hariç diğer clientler tetiklenir
![image](https://github.com/user-attachments/assets/615f8ebb-b7f7-4520-8bee-547c8c12738c)

#### Belirli Client'e Mesaj Gönderme
Bir clientten, belirli bir diğer client'e mesaj iletme.
![Belirli İstemciye Mesaj Gönderme](https://github.com/user-attachments/assets/6281a10e-ad9b-4341-8e2b-2712c6fb2982)

#### Grup Mesajlaşması
Grup üyelerine toplu mesaj gönderimi.
![Grup Mesajlaşması](https://github.com/user-attachments/assets/8eff48cc-f721-40b7-b37b-1a014ccc2d22)


#### ReceiveComplexMessageForAllClient() 
Product, complex type mesaj gondermek..
![image](https://github.com/user-attachments/assets/70b43f6a-385b-4d0d-a82d-c0958551bf8f)

#### ConsoleApp - 1 
SignalR Client -1, web tarafından gönderilen mesajı console ekranından görüntüleme
![image](https://github.com/user-attachments/assets/88ce5798-4d4e-49cd-9307-053a93799a82)

#### ConsoleApp - 2 
SignalR Client -2, console tarafından gönderilen mesaji web tarafinda görüntüleme
![image](https://github.com/user-attachments/assets/8382d291-38e7-4c69-bcae-98912b6c3c7a)

#### Worker App
SignalR Client, web tarafından gönderilen mesajin worker service tarafından görüntüleme
![image](https://github.com/user-attachments/assets/6eeaa82c-4f4b-4f6a-8ca4-8363e4b22a3f)


#### Veri Akışı (Streaming)
Data gönder by chunk (namesAsChunk)
![image](https://github.com/user-attachments/assets/062541b3-43ef-40af-a43b-588fcaf8b64e)

Product gönder by chunk (productAsChunk)
![image](https://github.com/user-attachments/assets/d970852a-1e4c-4181-8613-bca267305f0d)

Hub üzerinden veri parça parça aktarılır.
![Veri Akışı](https://github.com/user-attachments/assets/73c3f9d1-c002-4683-830d-fbdd46c932fb)

#### asp.net core hub api
![image](https://github.com/user-attachments/assets/e8f334b0-cf75-4b99-9070-80cc4921ffac)

#### Web tarafından mesajı görüntülemek
![image](https://github.com/user-attachments/assets/3d0c5ad9-adff-4f7f-b490-b82db3f46b9a)

---

## Örnek Proje

**Sample Project:** Bu bölümde, SignalR kullanılarak oluşturulan örnek proje ele alınmaktadır. Proje, temel SignalR senaryolarının yanı sıra Excel oluşturma işlemi gibi işlevsellikleri içermektedir. 
Proje, kullanıcıya gerçek zamanlı bildirimler sunmakta ve toast mesajları aracılığıyla dosya oluşturma işlemi tamamlandığında bilgilendirme sağlamaktadır.

### Özellikler

1. **SignalR ile Gerçek Zamanlı Bildirimler**:
   - Excel dosyası oluşturma işlemi tamamlandığında, SignalR aracılığıyla istemciye bildirim gönderilir.
   - Toast mesajları ile kullanıcı bilgilendirilir.
2. **Excel Dosyası İndirme**:
   - Oluşturulan Excel dosyası istemcinin indirebilmesi için bir bağlantı sağlar.
3. **In-Memory Queue (Kanal Kullanımı)**:
   - Veri işleme sırasında **channel** kullanılarak veriler kuyruk mantığıyla işlenir.
4. **ClosedXML Kütüphanesi**:
   - Excel dosyalarını oluşturmak için kullanılmıştır.

### Proje Ekran Görüntüleri

#### İlgili product listesinin excel çıktısını indirebilmek için önce giriş yapıyoruz.
![image](https://github.com/user-attachments/assets/6d79451f-973a-44d2-b7fa-436e0770f63c)
![image](https://github.com/user-attachments/assets/0ae6b0d5-6f8a-4166-ab1c-23f5a9b7cdd2)

#### Create Excel butonuna bastıktan sonra Toast message entegrasyonu ile kullanıcıyı bilgilendiriyoruz
![image](https://github.com/user-attachments/assets/0dd9fa61-a52d-48b8-b519-f591d0f7cec7)

#### CreateExcelBackgroundService sınıfında channel tabanlı işleme ile excel tablomuzu oluşturuyoruz
![image](https://github.com/user-attachments/assets/0ce56a22-8c27-4d7f-9af0-26907538d62a)

![image](https://github.com/user-attachments/assets/72a876fc-e075-4f17-a8f2-9d2ee361559f)
![image](https://github.com/user-attachments/assets/d134fc06-2875-4559-838c-bcea8ef49ed3)

---

## Teknolojiler ve Kütüphaneler

- **ASP.NET Core SignalR**
- **ClosedXML (Excel İşlemleri)**
- **Toast Mesajlar (Frontend)**

---

## Örnek Proje'nin Yapısı

**Backend:**
- SignalR Hub
- Channel tabanlı işleme
- Veri işleme ve Excel oluşturma.

**Frontend:**
- SignalR istemci bağlantıları
- Toast mesaj entegrasyonu
