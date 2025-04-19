import 'dart:convert';
import 'dart:typed_data';

class Compo {
  final int id;
  final String name;
  final int itemAmount; // اجعل هذا الحقل قابل للاحتواء بـ null
  final Uint8List image;

  Compo({
    required this.id,
    required this.name,
    required this.itemAmount, // لم يعد هذا الحقل مطلوبًا
    required this.image,
  });

  factory Compo.fromJson(Map<String, dynamic> json) {
    return Compo(
      id: json['id'], // تعيين القيمة الافتراضية إذا كانت null
      name: json['name'],
      itemAmount: json['itemAmount'], // تعيين القيمة الافتراضية إذا كانت null
      image: base64Decode(json['image']),
    );
  }
}
