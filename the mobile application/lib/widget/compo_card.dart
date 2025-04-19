import 'package:flutter/material.dart';
import 'package:aqttan2/model/compo.dart';

class CompoCard extends StatelessWidget {
  final Compo compo;
  final VoidCallback onTap; // Callback for the tap event

  const CompoCard({super.key, required this.compo, required this.onTap});

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      //  () {
      //   // الانتقال إلى صفحة عرض الأصناف
      //   // Navigator.push(
      //   //   context,
      //   //   MaterialPageRoute(
      //   //     builder: (context) => CompoItemsPage(compo: compo),
      //   //   ),
      //   // );
      // }, // Handle the tap event

      borderRadius: BorderRadius.circular(15),
      splashColor: Colors.blue.withOpacity(0.2),
      child: Card(
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(15),
        ),
        elevation: 8,
        shadowColor: Colors.black54,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            if (compo.image.isNotEmpty)
              ClipRRect(
                borderRadius:
                    const BorderRadius.vertical(top: Radius.circular(15)),
                child: Image.memory(
                  compo.image,
                  height: 150,
                  fit: BoxFit.cover,
                ),
              ),
            Padding(
              padding: const EdgeInsets.all(10.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    compo.name,
                    style: const TextStyle(
                      fontSize: 22,
                      fontWeight: FontWeight.bold,
                      fontFamily: 'SF',
                      color: Colors.black87,
                    ),
                  ),
                  const SizedBox(height: 5),
                  Text(
                    "${compo.itemAmount} items made of it",
                    style: TextStyle(
                      fontSize: 14,
                      fontFamily: 'SF',
                      color: Colors.grey[600],
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
