180202048 Osman ŞİMŞEK          180202054 Yener Emin ELİBOL
Proje başlığı:Asansörlerdeki Talep Yoğunluğunun Multithread ile Kontrolü
Asansörlerdeki Talep Yoğunluğunun Multithread ile kontrolü projesini çalıştırmak için gerekli program: visual studio 17.12 versiyonu
Asansörlerdeki Talep Yoğunluğunun Multithread ile kontrolü projesinin yazıldığı dil : c#
projeyi çalıştırmak için uygulanıcak adımlar: 

ilk olarak program çalıştığında programın kendine ait menüsü ekrana gelmektedir. 
Açılan ekranda;
           
	 ASANSÖRLERDEKİ TALEP YOĞUNLUĞUNUN MULTITHREAD İLE KONTROLÜ
 
----------------------PROJE ÇALIŞTIRILDIĞINDA---------------

Proje Başlangıç menüsü :

Basit bir arayüz karşılamaktadır. Bu arayüzde;

0.floor:queue:
1.floor:queue:
2.floor:queue:
3.floor:queue:
4.floor:queue:  
exit count
Kısımlarından katlarda bulunan kuyruklardaki insan sayısı gözükmektedir


active: asansörün aktif olup olmadığını görebilirsiniz. (True) ise asansör aktif (false) ise aktif değildir

mode:  Mode kısmından asansörün aktif yada beklemede olduğunu görüntüleyebilirsiniz. 
(working) asansörün çalışır durumda olduğunu, (idle) asansörün beklemede olduğunu gösterir.
floor: asansörün o an bulunduğu katı göstermektedir	
destination: asansörün gdeceği hedef katı gösterir
direction: asansörün hareket ettği yönü gösterir. (up) yukarı yöne, (down) aşağı.        
capacity: asansörün sahip olduğu kapasiteyi gösterir. default değer 10 dur.
count_inside: asansörde anlık olarak bulunan insan sayısını göstermektedir
inside: asansörde bulunan insanların hangi katlara gidiş yaptığını görüntüleyebilirsiniz

Asansör- : kısmından kaçıncı asansöre ait bilgiler olduğunu görebilirsiniz


0.floor:[[4(bekleyen kişi sayısı),4(hedef kat)],[3,3]]
1.floor:[]
2.floor:[]
3.floor:[]
4.floor:[]

bölümünden katlardaki insan kuyruğunu ve hedef katlarını görüntüleyebilirsiniz


Proje bileşenlerinin özellikleri:

AVM Giriş (Login) Thread: 500 ms zaman aralıklarıyla [1-10] arasında rastgele
sayıda müşterinin AVM’ye giriş yapmasını sağlamaktadır (Zemin Kat). Giren
müşterileri rastgele bir kata (1-4) gitmek için asansör kuyruğuna alır.

AVM Çıkış (Exit) Thread: 1000 ms zaman aralıklarıyla [1-5] arasında rastgele
sayıda müşterinin AVM’den çıkış yapmasını sağlamaktadır (Zemin Kat). Çıkmak
isteyen müşterileri rastgele bir kattan (1-4), zemin kata gitmek için asansör kuyruğuna
alır.
 
Asansör Thread : Katlardaki kuyrukları kontrol eder. Maksimum kapasiteyi
aşmayacak şekilde kuyruktaki müşterilerin talep ettikleri katlarda taşınabilmesini
sağlar. Bu thread asansör sayısı kadar (5 adet) olmalıdır.
NOT: Zemin kattan diğer katlara (AVM’ye) giriş yapmak isteyenler, ya da diğer
katlardan (AVM’den) çıkış yapmak isteyenler kuyruk oluştururlar.







