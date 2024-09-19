<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <h1>Microservice.Net6.Course</h1>
    <p>Bu proje, .NET 6.0 ile geliştirilen bir mikroservis projesidir. Basit olarak kursu görüntüleme, kurs kaydı oluşturma, kurs sepete ekleme ve satın alma işlemlerinden oluşmaktadır.</p>
    <h2>İçindekiler</h2>
    <ul>
        <li><a href="#about">Hakkında</a></li>
        <li><a href="#getting-started">Başlarken</a></li>
        <li><a href="#prerequisites">Gereksinimler</a></li>
        <li><a href="#installation">Kurulum</a></li>
        <li><a href="#usage">Kullanım</a></li>
    </ul>
    <h2 id="about">Hakkında</h2>
    <p>Bu proje, kursları görüntüleme, kayıt oluşturma, kursları sepete ekleme ve satın alma işlemlerini içermektedir. Yetkilendirme ve kullanıcı yönetimi için IdentityServer'ın ücretsiz sürümü kullanılmıştır. Servis endpointlerinin kullanımını kolaylaştırmak için bir Gateway eklenmiştir. Frontend tarafı ASP.NET Core MVC ile oluşturulmuş olup, MsSQL, Redis, MongoDB ve PostgreSQL gibi veritabanları entegre edilmiştir. Uygulama, Docker ile konteynerize edilerek dağıtım ve ölçeklenebilirlik sağlanmıştır.</p>
    <h2 id="getting-started">Başlarken</h2>
    <p>Bu projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyin.</p>
    <h2 id="prerequisites">Gereksinimler</h2>
    <ul>
        <li>Docker</li>
        <li>.NET 6 SDK</li>
    </ul>
    <h2 id="installation">Kurulum</h2>
    <ol>
        <li>Projeyi klonlayın:
            <pre><code>git clone https://github.com/GutsSword/Microservice.Net6.Course.git</code></pre>
        </li>
        <li>Proje dizinine gidin:
            <pre><code>cd Microservice.Net6.Course</code></pre>
        </li>
        <li>Docker konteynerlerini başlatın:
            <pre><code>docker-compose up</code></pre>
        </li>
    </ol>
    <h2 id="usage">Kullanım</h2>
    <p>Proje çalıştırıldıktan sonra, mikroservislerinizi yönetmek ve test etmek için aşağıdaki URL'leri kullanabilirsiniz:</p>
    <ul>
        <li><code>http://localhost:5000</code> - API Gateway</li>
        <li><code>http://localhost:5001</code> - Identity Service</li>
        <li><code>http://localhost:5002</code> - Product Service</li>
    </ul>
</body>
</html>
