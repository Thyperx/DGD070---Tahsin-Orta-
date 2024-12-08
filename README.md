# DGD070---Tahsin-Orta-
# Player Health System

## Özellikler
- **PlayerHealthComponent**: Oyuncunun sağlık değeri için bileşen.
- **PlayerDamagedComponent**: Oyuncuya hasar etkisini temsil eden tag bileşeni.
- **PlayerHealedComponent**: Oyuncunun iyileştirilmesini temsil eden tag bileşeni.

## Sistemler
1. **MyUniqueCreatePlayerHealthSystem**:
   - Oyuncu entity'sini oluşturur ve sağlık bileşeni ekler.
2. **MyUniqueChangePlayerHealthSystem**:
   - `D` tuşuna basıldığında hasar, `H` tuşuna basıldığında iyileştirme etkisi uygular.
3. **MyUniqueCheckPlayerHealthSystem**:
   - Sağlık değerini günceller ve bileşenleri kaldırır.

## Testler
- Sağlık değerinin `D` ve `H` tuşlarıyla değiştiği test edildi.
